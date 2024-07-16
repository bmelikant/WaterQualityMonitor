using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace WaterQualityMonitorApi.V1.PH;

public static class PHEndpointExtensions {

	public static RouteGroupBuilder AddPHEndpoints(this RouteGroupBuilder routeGroup) {

		routeGroup.MapGet("/ph", async (
			[FromServices] ILoggerFactory loggerFactory,
			ClaimsPrincipal user
		) => {

			var logger = loggerFactory.CreateLogger(typeof(PHEndpointExtensions));
			logger.LogInformation("Attempting to load PH readings");
		})
		.WithOpenApi()
		.Produces<IEnumerable<PHReadingResponseDTO>>()
		.RequireAuthorization();

		return routeGroup;
	}
}