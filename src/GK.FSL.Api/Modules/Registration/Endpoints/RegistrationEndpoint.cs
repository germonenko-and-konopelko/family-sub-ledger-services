using FastEndpoints;
using GK.FSL.Api.Modules.Registration.Models;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;

namespace GK.FSL.Api.Modules.Registration.Endpoints;

public class RegistrationEndpoint(IUserRegistrationService registrationService) : Endpoint<RegistrationRequest>
{
    public override void Configure()
    {
        Post("api/registration");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegistrationRequest req, CancellationToken ct)
    {
        var registrationDto = new RegisterUserDto
        {
            EmailAddress = req.EmailAddress,
            FirstName = req.FirstName,
            LastName = req.LastName,
            Password = req.Password
        };

        await registrationService.RegisterUserAsync(registrationDto);
        await SendNoContentAsync(ct);
    }
}