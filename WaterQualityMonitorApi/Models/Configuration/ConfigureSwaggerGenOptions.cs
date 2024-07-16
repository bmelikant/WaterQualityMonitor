using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WaterQualityMonitorApi.Models.Configuration;

public class ConfigureSwaggerGenOptions : IConfigureNamedOptions<SwaggerGenOptions> {
	private readonly IApiVersionDescriptionProvider _descriptionProvider;

	public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider descriptionProvider)
		=> _descriptionProvider = descriptionProvider;

	public void Configure(string? name, SwaggerGenOptions options) {
		Configure(options);
	}

	public void Configure(SwaggerGenOptions options) {
		/* describe API versions */
		foreach (ApiVersionDescription description in _descriptionProvider.ApiVersionDescriptions) {
			OpenApiInfo openApiInfo = new() {
				Title = $"WaterQualityMonitor.API.V{description.ApiVersion}",
				Version = description.ApiVersion.ToString()
			};

			options.SwaggerDoc(description.GroupName, openApiInfo);
		}

		/* describe bearer token input */
		options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
			In = ParameterLocation.Header,
			Description = "JWT Bearer token field",
			Name = "Authorization",
			Type = SecuritySchemeType.ApiKey
		});

		/* describe security requirement */
		options.AddSecurityRequirement(new OpenApiSecurityRequirement {
			{
				new OpenApiSecurityScheme {
					Reference = new OpenApiReference {
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				},
				new string [] {}
			}
		});
	}
}