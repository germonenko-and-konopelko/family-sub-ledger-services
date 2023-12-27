﻿using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Authorization.Models;

namespace GK.FSL.Api.Modules.Authorization;

[JsonSerializable(typeof(SignInByRequest))]
[JsonSerializable(typeof(AuthorizationResponse))]
public partial class AuthorizationSerializationContext : JsonSerializerContext;