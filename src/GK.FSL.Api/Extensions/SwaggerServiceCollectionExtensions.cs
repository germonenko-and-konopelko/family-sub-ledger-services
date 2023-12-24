using System.Text.Json;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using GK.FSL.Api.Constants;

namespace GK.FSL.Api.Extensions;

public static class SwaggerServiceCollectionExtensions
{
    public static void AddSwaggerDocuments(this IServiceCollection services)
    {
        const string title = "G&K Sub-Ledger API";

        services.SwaggerDocument(options =>
        {
            options.AutoTagPathSegmentIndex = 0;
            options.ShortSchemaNames = true;
            options.EndpointFilter = endpoint => endpoint.EndpointTags?.Contains(ApiVersions.PreviewV1) == true;

            options.DocumentSettings = document =>
            {
                document.Title = title;
                document.DocumentName = ApiVersions.PreviewV1;
                document.Version = ApiVersions.PreviewV1;
            };

            options.SerializerSettings = json =>
            {
                json.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                json.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            };
        });
    }
}