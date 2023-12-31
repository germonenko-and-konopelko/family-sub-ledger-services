using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Authorization.Models;

namespace GK.FSL.Api.Modules.Authorization;

[JsonSerializable(typeof(AuthorizationResponse))]
[JsonSerializable(typeof(SignInRequest))]
public partial class AuthorizationSerializationContext : JsonSerializerContext;