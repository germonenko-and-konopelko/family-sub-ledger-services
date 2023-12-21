using FastEndpoints;
using GK.FSL.Api.Modules.Registration.Models;

namespace GK.FSL.Api.Modules.Registration.Endpoints;

public class RegistrationEndpoint : Endpoint<RegistrationRequest>
{
    public override void Configure()
    {
        Post("api/registration");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegistrationRequest req, CancellationToken ct)
    {
        await SendNoContentAsync(ct);
    }
}