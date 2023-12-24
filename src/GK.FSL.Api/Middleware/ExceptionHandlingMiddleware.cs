using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Common.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

using ErrorMessages = GK.FSL.Api.Resources.GeneralErrorMessages;
using ValidationException = GK.FSL.Common.Validation.ValidationException;

namespace GK.FSL.Api.Middleware;

public class ExceptionHandlingMiddleware(
    IStringLocalizer<ErrorMessages> errorMessages,
    ILogger<ExceptionHandlingMiddleware> logger,
    IOptions<JsonOptions> jsonOptions
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            var errorTitle = errorMessages[nameof(ErrorMessages.ValidationError)];
            var errorDetail = errorMessages[nameof(ErrorMessages.ValidationErrorDetails)];

            var response = new ApiProblem(
                StatusCodes.Status400BadRequest,
                errorTitle,
                errorDetail,
                e.Errors.Select(err => new FieldError(err.Field, err.Message))
            );

            await SendResponseAsync(response, context);
        }
        catch(Exception e)
        {
            logger.LogError(
                EventIds.ServerError,
                e,
                "Server error occurred, trace ID: {TraceId}", context.TraceIdentifier);

            var errorTitle = errorMessages[nameof(ErrorMessages.UnknownServerError)];
            var errorDetail = errorMessages[nameof(ErrorMessages.UnknownServerErrorDetails)];

            var response = new ApiProblem(
                StatusCodes.Status500InternalServerError,
                errorTitle,
                errorDetail,
                context.TraceIdentifier
            );

            await SendResponseAsync(response, context);
        }
    }

    private Task SendResponseAsync(ApiProblem problem, HttpContext context)
        => Results
            .Json(problem, jsonOptions.Value.SerializerOptions, MediaTypes.Json, problem.Status)
            .ExecuteAsync(context);
}