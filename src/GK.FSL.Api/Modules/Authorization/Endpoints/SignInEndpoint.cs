using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Authorization.Models;

namespace GK.FSL.Api.Modules.Authorization.Endpoints;

public class SignInEndpoint : Endpoint<SignInByLoginAndPasswordRequest, AuthorizationResult>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/sign-in");
        Tags(ApiVersions.PreviewV1);
    }

    public override Task HandleAsync(SignInByLoginAndPasswordRequest req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
}