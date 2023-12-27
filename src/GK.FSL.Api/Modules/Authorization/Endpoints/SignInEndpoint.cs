using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Authorization.Models;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;

namespace GK.FSL.Api.Modules.Authorization.Endpoints;

public class SignInEndpoint(ISignInService signInService) : Endpoint<SignInRequest, AuthorizationResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/sign-in");
        Tags(ApiVersions.PreviewV1);

        Description(description =>
        {
            description.WithTags("Authorization");
            description.Accepts<SignInRequest>(MediaTypes.Json);
            description.Produces<AuthorizationResponse>(StatusCodes.Status200OK, MediaTypes.Json);
            description.Produces<ApiProblem>(StatusCodes.Status400BadRequest, MediaTypes.JsonProblem);
        });

        Summary(summary =>
        {
            summary.Summary = "Sign in by user credentials";
            summary.ExampleRequest = new SignInRequest
            {
                Login = "emilia.müller@gmail.com",
                Password = "1234",
                DeviceName = "swagger",
            };
            summary.Responses[200] = "Access and refresh tokens";
            summary.ResponseExamples[200] = new AuthorizationResponse
            {
                ExpiresTimestamp = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeMilliseconds()
            };
            summary.ResponseExamples[400] = ApiProblem.GetSwaggerExample(
                null,
                [
                    new FieldError(nameof(SignInRequest.Login), "Login or password is invalid"),
                    new FieldError(nameof(SignInRequest.Password), "Login or password is invalid"),
                ]
            );
            summary.Responses[400] = "Validation error";
        });
    }

    public override async Task HandleAsync(SignInRequest req, CancellationToken ct)
    {
        var dto = new SignInDto
        {
            Login = req.Login,
            Password = req.Password,
            DeviceName = req.DeviceName,
            IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
        };

        var result = await signInService.SignInAsync(dto);
        var response = new AuthorizationResponse
        {
            AccessToken = result.AccessToken,
            RefreshToken = result.RefreshToken,
            Type = result.Type,
            ExpiresTimestamp = result.Expires.ToUnixTimeMilliseconds()
        };

        await SendOkAsync(response, ct);
    }
}