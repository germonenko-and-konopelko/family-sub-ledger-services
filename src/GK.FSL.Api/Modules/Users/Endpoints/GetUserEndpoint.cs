using FastEndpoints;
using GK.FSL.Api.Modules.Users.Models;
using GK.FSL.Common.Cryptography.Contracts;

namespace GK.FSL.Api.Modules.Users.Endpoints;

public class GetUserEndpoint(IEncoder<long> encoder) : EndpointWithoutRequest<UserModel>
{
    public override void Configure()
    {
        Get("/api/users/{id}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var userIdParam = Route<long>("id");
        return SendOkAsync(new UserModel
        {
            Id = encoder.Encode(userIdParam),
            FirstName = "Slava",
            LastName = "Germonenko",
            EmailAddress = "slava.germonenko@gmail.com",
            SignUpDate = DateOnly.FromDateTime(DateTime.Now),
        }, ct);
    }
}