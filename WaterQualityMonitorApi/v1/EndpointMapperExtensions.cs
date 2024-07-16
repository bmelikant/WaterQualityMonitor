using Asp.Versioning;
using Asp.Versioning.Builder;
using WaterQualityMonitorApi.V1.Login;
using WaterQualityMonitorApi.V1.PH;

namespace WaterQualityMonitorApi.V1;

public static class EndpointMapperExtensions {

	public static void MapEndpoints_V1(this WebApplication app) {
		ApiVersionSet versionSet = app.NewApiVersionSet()
			.HasApiVersion(new ApiVersion(1))
			.ReportApiVersions()
			.Build();

		app.MapGroup("/v{apiVersion:apiVersion}")
			.WithApiVersionSet(versionSet)
			.AddLoginEndpoints()
			.AddPHEndpoints();
	}
}