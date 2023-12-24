using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using GK.FSL.Api.Extensions;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Api.Resources;
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
using Microsoft.Extensions.Localization;
using Microsoft.FeatureManagement;
using Sqids;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddFeatureManagement();

builder.Services.AddOptions<HashingOptions>()
    .Bind(builder.Configuration.GetSection("Security:Hashing"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddLocalization();
builder.Services.AddRequestLocalization(_ => {});

builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocuments();

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
    options.Endpoints.RoutePrefix = "api";
    options.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.Serializer.Options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

    // Let's make it compatible with this spec: https://datatracker.ietf.org/doc/html/rfc7807
    options.Errors.ResponseBuilder = (validationFailures, context, statusCode) =>
    {
        var errorMessages = context.RequestServices.GetRequiredService<IStringLocalizer<GeneralErrorMessages>>();
        var errors = validationFailures.Select(e => new FieldError(e.PropertyName, e.ErrorMessage));
        return new ApiProblem(
            statusCode,
            errorMessages[nameof(GeneralErrorMessages.ValidationError)],
            errorMessages[nameof(GeneralErrorMessages.ValidationErrorDetails)],
            errors
        );
    };
});

app.UseSwaggerGen();

app.Run();
