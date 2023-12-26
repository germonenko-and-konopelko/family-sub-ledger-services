using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Authorization.Models;

namespace GK.FSL.Api.Modules.Authorization;

[JsonSerializable(typeof(SignInByLoginAndPasswordRequest))]
[JsonSerializable(typeof(AuthorizationResult))]
public partial class AuthorizationSerializationContext : JsonSerializerContext;