using FastEndpoints;
using FluentValidation;
using GK.FSL.Api.Modules.Registration.Models;
using GK.FSL.Api.Resources;
using Microsoft.Extensions.Localization;

using ErrorMessages = GK.FSL.Api.Resources.ModelValidationErrorMessages;

namespace GK.FSL.Api.Modules.Registration.Validators;

public class RegistrationRequestValidator : Validator<RegistrationRequest>
{
    public RegistrationRequestValidator(IStringLocalizer<ErrorMessages> errorMessages)
    {
        RuleFor(req => req.FirstName)
            .NotNull()
            .NotEmpty()
            .WithMessage(errorMessages[nameof(ErrorMessages.FirstNameIsRequired)]);

        RuleFor(req => req.FirstName)
            .MaximumLength(50)
            .WithMessage(errorMessages[nameof(ErrorMessages.FirstNameMaxLength)]);

        RuleFor(req => req.LastName)
            .NotNull()
            .NotEmpty()
            .WithMessage(errorMessages[nameof(ErrorMessages.LastNameIsRequired)]);

        RuleFor(req => req.LastName)
            .MaximumLength(50)
            .WithMessage(errorMessages[nameof(ErrorMessages.LastNameMaxLength)]);

        RuleFor(req => req.EmailAddress)
            .NotNull()
            .NotEmpty()
            .WithMessage(errorMessages[nameof(ErrorMessages.EmailAddressIsRequired)]);

        RuleFor(req => req.EmailAddress)
            .MaximumLength(250)
            .WithMessage(errorMessages[nameof(ErrorMessages.EmailAddressMaxLength)]);

        RuleFor(req => req.EmailAddress)
            .EmailAddress()
            .WithMessage(errorMessages[nameof(ErrorMessages.EmailAddressIsMalformed)]);

        RuleFor(req => req.Password)
            .NotNull()
            .NotEmpty()
            .WithMessage(errorMessages[nameof(ErrorMessages.PasswordIsRequired)]);

        RuleFor(req => req.Password)
            .MaximumLength(200)
            .WithMessage(errorMessages[nameof(ModelValidationErrorMessages.PasswordMaxLength)]);
    }
}