using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Authorization.Models;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Api.Options;
using GK.FSL.Api.Resources;
using GK.FSL.Auth.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace GK.FSL.Api.Modules.Authorization.Endpoints;

public class RefreshAccessTokenEndpoint(
    IOptionsSnapshot<CookiesOptions> cookiesOptions,
    IStringLocalizer<GeneralErrorMessages> errorMessages,
    ISessionRefreshService sessionRefreshService
) : EndpointWithoutRequest<Results<Ok<AuthorizationResponse>, BadRequest<ApiProblem>>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/auth/refresh-token");
        Tags(ApiVersions.PreviewV1);

        Description(description =>
        {
            description.WithTags(ApiModuleNames.Authorization);
            description.Produces<AuthorizationResponse>(StatusCodes.Status200OK, MediaTypes.Json);
            description.Produces<ApiProblem>(StatusCodes.Status400BadRequest, MediaTypes.JsonProblem);
        });

        Summary(summary =>
        {
            summary.Summary = "Refresh a refresh token";
            summary.Description = "The refresh token can be provided via the query or the cookies";
            summary.Responses[StatusCodes.Status200OK] = "Access and refresh tokens";
            summary.ResponseExamples[StatusCodes.Status200OK] = AuthorizationResponse.GetSwaggerExample();
            summary.Responses[StatusCodes.Status400BadRequest] = "Validation error";
            summary.Responses[StatusCodes.Status422UnprocessableEntity] = "Error related to refreshing token.";
            summary.ResponseExamples[400] = ApiProblem.GetSwaggerExample();
            summary.ResponseExamples[422] = new ApiProblem(
                StatusCodes.Status422UnprocessableEntity,
                "Refresh token was not provided.",
                "Refresh token was not provided. It can be provided via the cookies using refresh_token cookie entry."
            );
        });
    }

    public override async Task<Results<Ok<AuthorizationResponse>, BadRequest<ApiProblem>>> ExecuteAsync(CancellationToken ct)
    {
        var tokenFound =
            HttpContext.Request.Cookies.TryGetValue(cookiesOptions.Value.Sessions.Name, out var refreshToken);
        if(!tokenFound || refreshToken is null)
        {
            return TypedResults.BadRequest(
                new ApiProblem(
                    StatusCodes.Status400BadRequest,
                    errorMessages[nameof(GeneralErrorMessages.RefreshTokenIsNotProvided)],
                    errorMessages[nameof(GeneralErrorMessages.RefreshTokenIsNotProvidedDetails)]
                )
            );
        }

        var refreshResult = await sessionRefreshService.RefreshSessionAsync(new(){ RefreshToken = refreshToken });

        HttpContext.Response.Cookies.Append(
            cookiesOptions.Value.Sessions.Name,
            refreshResult.RefreshToken.Value,
            new CookieOptions
            {
                HttpOnly = cookiesOptions.Value.Sessions.HttpOnly,
                SameSite = cookiesOptions.Value.Sessions.SameSite,
                Secure = cookiesOptions.Value.Sessions.Secure,
            }
        );

        return TypedResults.Ok(new AuthorizationResponse
        {
            AccessToken = refreshResult.AccessToken.Value,
            RefreshToken = refreshResult.RefreshToken.Value,
            ExpiresTimestamp = refreshResult.AccessToken.Expires.ToUnixTimeMilliseconds(),
        });
    }
}