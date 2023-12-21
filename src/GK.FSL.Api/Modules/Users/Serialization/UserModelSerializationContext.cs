using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Users.Models;

namespace GK.FSL.Api.Modules.Users.Serialization;

[JsonSerializable(typeof(UserModel))]
public partial class UserModelSerializationContext : JsonSerializerContext
{
}