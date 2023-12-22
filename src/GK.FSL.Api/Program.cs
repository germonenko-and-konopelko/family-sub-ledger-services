using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using GK.FSL.Api.Services;
using GK.FSL.Api.Services.Contracts;
using GK.FSL.Common.Cryptography;
using GK.FSL.Common.Validation;
using GK.FSL.Common.Validation.Contracts;
using GK.FSL.Core;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;
using GK.FSL.Registration.Services;
using GK.FSL.Registration.Validators;
using Microsoft.EntityFrameworkCore;
using Sqids;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddOptions<HashingOptions>()
    .Bind(builder.Configuration.GetSection("Security:Hashing"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<CoreDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Core");
    options.UseNpgsql(connectionString, postgres =>
    {
        postgres.MigrationsAssembly("GK.FSL.Migrations");
        postgres.MigrationsHistoryTable("migrations", "system");
    });
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
    options.EnableDetailedErrors(builder.Environment.IsDevelopment());
});


builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddSingleton<IHasher, Pbkdf2Hasher>();
builder.Services.AddSingleton<IIdEncoder, SqidsIdEncoder>();
builder.Services.AddSingleton<SqidsEncoder<long>>(_ =>
{
    var options = new SqidsOptions();
    var alphabet = builder.Configuration.GetValue<string>("Security:Sqids:Alphabet");
    if (!string.IsNullOrEmpty(alphabet))
    {
        options.Alphabet = alphabet;
    }

    var minLength = builder.Configuration.GetValue<int>("Security:Sqids:MinLength");
    if (minLength >= 0)
    {
        options.MinLength = minLength;
    }

    return new SqidsEncoder<long>(options);
});

builder.Services.AddScoped(typeof(IValidationRunner<>), typeof(ValidationRunner<>));
builder.Services.AddScoped<IAsyncValidator<RegisterUserDto>, UserEmailInUseValidator>();

var app = builder.Build();

app.UseFastEndpoints(options =>
{
    options.Versioning.Prefix = "v";
    options.Serializer.Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
});

app.Run();
