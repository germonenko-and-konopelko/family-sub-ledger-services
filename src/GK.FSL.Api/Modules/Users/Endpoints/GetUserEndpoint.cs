using FastEndpoints;
using GK.FSL.Api.Modules.Users.Models;

namespace GK.FSL.Api.Modules.Users.Endpoints;

public class GetUserEndpoint : EndpointWithoutRequest<UserModel>
{
    public override void Configure()
    {
        Get("/api/users/{id}");
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}