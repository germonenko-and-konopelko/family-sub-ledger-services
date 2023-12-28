using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.Swagger;
using GK.FSL.Api.Extensions;
using GK.FSL.Api.Middleware;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Api.Resources;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Options;
using GK.FSL.Auth.Services;
using GK.FSL.Common.Cryptography;
using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Common.Cryptography.Options;
using GK.FSL.Common.Validation;
using GK.FSL.Common.Validation.Contracts;
using GK.FSL.Core;
using GK.FSL.Core.Services;
using GK.FSL.Core.Services.Contracts;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;
using GK.FSL.Registration.Services;
using GK.FSL.Registration.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.FeatureManagement;
using Sqids;

var builder = WebApplication.CreateSlimBuilder(args);

// Options and feature management
builder.Services.AddFeatureManagement();

builder.Services
    .AddOptions<HashingOptions>()
    .Bind(builder.Configuration.GetSection("Security:Hashing"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<AccessTokenOptions>()
    .Bind(builder.Configuration.GetSection("Security:AccessToken"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services
    .AddOptions<SqidsOptions>()
    .Configure(options =>
    {
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
    });

// Built-in stuff and third-party libraries
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

// Auth
builder.Services.AddSingleton<IAccessTokenGenerator, JwtAccessTokenGenerator>();
builder.Services.AddSingleton<IRefreshTokenGenerator, RandomRefreshTokenGenerator>();
builder.Services.AddScoped<ISignInService, SignInService>();

// Sign in and registration
builder.Services.AddScoped<IUserRegistrationService, UserRegistrationService>();
builder.Services.AddScoped<ISignInService, SignInService>();

// Core
builder.Services.AddScoped<IEntityResolver, EntityResolver>();

// Validation
builder.Services.AddScoped(typeof(IValidationRunner<>), typeof(ValidationRunner<>));
builder.Services.AddScoped<IAsyncValidator<RegisterUserDto>, UserEmailInUseValidator>();

// Cryptography
builder.Services.AddSingleton<IHasher, Pbkdf2Hasher>();
builder.Services.AddSingleton(typeof(IEncoder<>), typeof(SqidsBasedEncoder<>));

// Class-based middleware
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// Finally, the pipeline
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
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
