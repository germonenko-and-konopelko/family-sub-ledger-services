using FastEndpoints;
using FluentValidation;
using GK.FSL.Api.Modules.Authorization.Models;
using Microsoft.Extensions.Localization;

using ErrorMessages = GK.FSL.Api.Resources.ModelValidationErrorMessages;

namespace GK.FSL.Api.Modules.Authorization.Validators;

public class SignInByRequestValidator : Validator<SignInRequest>
{
    public SignInByRequestValidator(IStringLocalizer<ErrorMessages> errorMessages)
    {
        RuleFor(req => req.Login)
            .NotEmpty()
            .NotNull()
            .WithMessage(errorMessages[nameof(ErrorMessages.LoginIsRequired)]);

        RuleFor(req => req.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage(errorMessages[nameof(ErrorMessages.PasswordIsRequired)]);
    }
}