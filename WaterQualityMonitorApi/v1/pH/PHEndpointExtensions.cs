using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;
using WaterQualityMonitorApi.Models.Permissions;

namespace WaterQualityMonitorApi.V1.PH;

public static class PHEndpointExtensions {

	public static RouteGroupBuilder AddPHEndpoints(this RouteGroupBuilder routeGroup) {

		var phMeasurementsRouteGroup = routeGroup.MapGroup("/ph")
			.WithTags("pH Readings")
			.WithDescription("Endpoints for getting pH measurements from devices");

		///<summary>
		/// Get pH device measurements for the authorized user
		/// Polls pH measurements from all devices belonging to the given user
		/// TODO: make this endpoint require an active subscription
		///</summary>
		phMeasurementsRouteGroup.MapGet("/measurements", async (
			[FromServices] ILoggerFactory loggerFactory,
			[FromServices] WaterMonitorDbContext dbContext,
			ClaimsPrincipal user
		) => {
			var logger = loggerFactory.CreateLogger(typeof(PHEndpointExtensions));
			logger.LogInformation("Get pH measurements");

			var username = user.Identity?.Name ?? "";
			// it'd be very odd if we got here, since this
			// endpoint requires authorization
			if (string.IsNullOrWhiteSpace(username)) {
				return Results.Json(new { 
					message = "Could not determine username from request"
				}, statusCode: 401);
			}

			var phReadings = await dbContext.Measurements
				.Where(measurement => 
					measurement.Type.Name == "pH" &&
					measurement.Device.Owner.UserName == username
				)
				.Select(measurement => new PHReadingResponseDTO {
					ReadingDateAndTime = measurement.MeasuredTime,
					Value = measurement.Value.ToString()
				})
				.ToListAsync();

			return Results.Ok(phReadings);
		})
		.WithOpenApi()
		.WithDescription("Get pH measurements from all user devices")
		.Produces<IEnumerable<PHReadingResponseDTO>>()
		.RequireAuthorization(
			Permissions.API_Access
		);

		phMeasurementsRouteGroup.MapGet("/measurements/{deviceGuid}", async (
			[FromServices] ILoggerFactory loggerFactory,
			[FromServices] WaterMonitorDbContext dbContext,
			[FromQuery] string deviceGuid
		) => {

			
		})
		.WithOpenApi()
		.WithDescription("Get pH measurements from specified user device")
		.Produces<IEnumerable<PHReadingResponseDTO>>()
		.RequireAuthorization(
			Permissions.API_Access
		);

		return routeGroup;
	}
}