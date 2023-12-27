using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Authorization.Models;
using GK.FSL.Auth.Contracts;
using GK.FSL.Auth.Models;

namespace GK.FSL.Api.Modules.Authorization.Endpoints;

public class SignInEndpoint(ISignInService signInService) : Endpoint<SignInByRequest, AuthorizationResponse>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/sign-in");
        Tags(ApiVersions.PreviewV1);
    }

    public override async Task HandleAsync(SignInByRequest req, CancellationToken ct)
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