using System.Text.Json.Serialization;

namespace GK.FSL.Api.Modules.Common.Models;

public record ApiProblem(int Status, string Title, string Detail)
{
    public ApiProblem(int Status, string Title, string Detail, string traceId) : this(Status, Title, Detail)
    {
        TraceId = traceId;
    }

    public ApiProblem(int Status, string Title, string Detail, IEnumerable<FieldError> errors)
        : this(Status, Title, Detail)
    {
        Errors = errors;
    }

    public ApiProblem(int Status, string Title, string Detail, string traceId, IEnumerable<FieldError> errors)
        : this(Status, Title, Detail)
    {
        TraceId = traceId;
        Errors = errors;
    }

    public int Status { get; set; } = Status;

    public string Title { get; set; } = Title;

    public string Detail { get; set; } = Detail;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<FieldError>? Errors { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TraceId { get; set; }

    public static ApiProblem GetSwaggerExample(string? traceId = null, IEnumerable<FieldError>? errors = null)
        => new(
            400,
            "One or more validation errors occurred.",
            "One or more errors occured while validating the request. This may be relation to the malformed data. For more details please see the 'errors' section."
            ) { TraceId = traceId, Errors = errors };
}

public record FieldError(string Field, string Message);