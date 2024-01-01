using GK.FSL.Common.Validation;
using GK.FSL.Common.Validation.Contracts;
using GK.FSL.Core;
using GK.FSL.Registration.Models;
using GK.FSL.Registration.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GK.FSL.Registration.Validators;

public class UserEmailInUseValidator(
    CoreDbContext databaseContext,
    IStringLocalizer<ErrorMessages> errorMessage
) : IAsyncValidator<RegisterUserDto>
{
    public async Task<bool> TryValidateAsync(RegisterUserDto model, ICollection<ValidationError> errors)
    {
        var userEmailIsInUse = await databaseContext.Users.AnyAsync(user => user.EmailAddress == model.EmailAddress);
        if (!userEmailIsInUse)
        {
            return true;
        }

        var error = new ValidationError(
            nameof(RegisterUserDto.EmailAddress),
            errorMessage[nameof(ErrorMessages.EmailIsInUse)]
        );
        errors.Add(error);

        return false;
    }
}