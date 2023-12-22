using GK.FSL.Common.Validation;
using GK.FSL.Common.Validation.Contracts;
using GK.FSL.Core;
using GK.FSL.Registration.Constants;
using GK.FSL.Registration.Models;
using Microsoft.EntityFrameworkCore;

namespace GK.FSL.Registration.Validators;

public class UserEmailInUseValidator(CoreDbContext databaseContext) : IAsyncValidator<RegisterUserDto>
{
    public async Task<bool> TryValidateAsync(RegisterUserDto model, ICollection<ValidationError> errors)
    {
        var userEmailIsInUse = await databaseContext.Users.AnyAsync(user => user.EmailAddress == model.EmailAddress);
        if (userEmailIsInUse)
        {
            errors.Add(new (ErrorCodes.EmailInUse, ""));
            return false;
        }

        return true;
    }
}