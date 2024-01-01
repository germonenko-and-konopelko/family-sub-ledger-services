using System.Text.Json.Serialization;
using GK.FSL.Api.Modules.Common.Models;

namespace GK.FSL.Api.Modules.Common;

[JsonSerializable(typeof(ApiProblem))]
public partial class SerializationContext : JsonSerializerContext;