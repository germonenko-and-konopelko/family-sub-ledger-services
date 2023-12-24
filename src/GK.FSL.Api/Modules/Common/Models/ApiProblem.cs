using System.Text.Json.Serialization;

namespace GK.FSL.Api.Modules.Common.Models;

public record ApiProblem(
    int Status,
    string Title,
    string Detail,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] IEnumerable<FieldError>? Errors = null);

public record FieldError(string Field, string Message);