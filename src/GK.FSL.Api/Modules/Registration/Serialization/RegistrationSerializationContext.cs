using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Registration.Models;

namespace GK.FSL.Api.Modules.Registration.Serialization;

[JsonSerializable(typeof(RegistrationRequest))]
public partial class RegistrationSerializationContext : JsonSerializerContext
{
}