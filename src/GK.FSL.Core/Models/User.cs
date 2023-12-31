using System.ComponentModel.DataAnnotations;
using GK.FSL.Core.Models.Contracts;

namespace GK.FSL.Core.Models;

public class User : BaseModel<long>
{
    [Required, StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(250), EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    public UserStatus Status { get; set; }

    public Password? Password { get; set; }

    public void SetPassword(byte[] hash, byte[] salt)
    {
        Password = new()
        {
            Hash = hash,
            Salt = salt,
        };
    }
}

public enum UserStatus
{
    None = 0,
    Inactive = 5,
    Active = 10,
}

public class Password
{
    [MaxLength(400)]
    public byte[] Hash { get; set; } = Array.Empty<byte>();

    [MaxLength(400)]
    public byte[] Salt { get; set; } = Array.Empty<byte>();
}