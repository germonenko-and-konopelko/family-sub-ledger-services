using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Authorization.Models;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Api.Options;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;
using Microsoft.Extensions.Options;

namespace GK.FSL.Api.Modules.Authorization.Endpoints;

public class SignInEndpoint(
    IOptionsSnapshot<CookiesOptions> cookiesOptions,
    ISignInService signInService
) : Endpoint<SignInRequest, AuthorizationResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/auth/sign-in");
        Tags(ApiVersions.PreviewV1);

        Description(description =>
        {
            description.WithTags(ApiModuleNames.Authorization);
            description.Accepts<SignInRequest>(MediaTypes.Json);
            description.Produces<AuthorizationResponse>(StatusCodes.Status200OK, MediaTypes.Json);
            description.Produces<ApiProblem>(StatusCodes.Status400BadRequest, MediaTypes.JsonProblem);
        });

        Summary(summary =>
        {
            summary.Summary = "Sign in by user credentials";
            summary.ExampleRequest = SignInRequest.GetSwaggerExample();
            summary.Responses[200] = "Access and refresh tokens";
            summary.ResponseExamples[200] = AuthorizationResponse.GetSwaggerExample();
            summary.Responses[400] = "Validation error";
            summary.ResponseExamples[400] = ApiProblem.GetSwaggerExample(
                null,
                [
                    new FieldError(nameof(SignInRequest.Login), "Login or password is invalid"),
                    new FieldError(nameof(SignInRequest.Password), "Login or password is invalid"),
                ]
            );
        });
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        var dto = new SignInDto
        {
            Login = req.Login,
            Password = req.Password,
            DeviceName = req.Device,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
        };

        var result = await signInService.SignInAsync(dto);

        HttpContext.Response.Cookies.Append(
            cookiesOptions.Value.Sessions.Name,
            result.RefreshToken.Value,
            new CookieOptions
            {
                HttpOnly = cookiesOptions.Value.Sessions.HttpOnly,
                SameSite = cookiesOptions.Value.Sessions.SameSite,
                Secure = cookiesOptions.Value.Sessions.Secure,
            }
        );

        await SendOkAsync(new()
        {
            AccessToken = result.AccessToken.Value,
            RefreshToken = result.RefreshToken.Value,
            ExpiresTimestamp = result.AccessToken.Expires.ToUnixTimeMilliseconds(),
        }, ct);
    }
}