using FastEndpoints;
using GK.FSL.Api.Constants;
using GK.FSL.Api.Modules.Common.Models;
using GK.FSL.Api.Modules.Registration.Models;
using GK.FSL.Registration.Contracts;
using GK.FSL.Registration.Models;
using Microsoft.FeatureManagement;

using FeatureFlags = GK.FSL.Registration.Constants.FeatureFlags;

namespace GK.FSL.Api.Modules.Registration.Endpoints;

public class RegistrationEndpoint(
    IFeatureManager featureManager,
    IUserRegistrationService registrationService
) : Endpoint<RegistrationRequest>
{
    public override void Configure()
    {
        AllowAnonymous();
        Post($"{ApiVersions.PreviewV1}/registration");
        Tags(ApiVersions.PreviewV1);

        Description(description =>
        {
            description.WithTags("Registration");
            description.ClearDefaultProduces(200);
            description.Accepts<RegistrationRequest>(MediaTypes.Json);
            description.Produces<ApiProblem>(StatusCodes.Status400BadRequest, MediaTypes.JsonProblem);
        });
        Summary(summary =>
        {
            summary.Summary = "Registers a new user";
            summary.ExampleRequest = new RegistrationRequest
            {
                FirstName = "Emilia",
                LastName = "Müller",
                EmailAddress = "emilia.müller@gmail.com",
                Password = "1234"
            };
            summary.Responses[204] = "Successful no-content response";
            summary.Responses[400] = "Validation error";
        });
    }

    public override async Task HandleAsync(RegistrationRequest req, CancellationToken ct)
    {
        var registrationIsOpened = await featureManager.IsEnabledAsync(FeatureFlags.AutomaticUserActivation);
        if (!registrationIsOpened)
        {
            await SendNotFoundAsync(ct);
            return;
        }

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