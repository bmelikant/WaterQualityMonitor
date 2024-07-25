using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;
using WaterQualityMonitorApi.Database.Models;
using WaterQualityMonitorApi.Models.Constants;
using WaterQualityMonitorApi.V1.Device.Models;

namespace WaterQualityMonitorApi.V1.Device;

public static class DeviceEndpointExtensions {

	public static void AddDeviceEndpoints(this RouteGroupBuilder routeGroupBuilder) {

		routeGroupBuilder.MapPost("/device/status", (

		) => {
			
		});

		routeGroupBuilder.MapPost("/device/register", async (
			[FromServices] WaterMonitorDbContext dbContext,
			[FromBody] AddDeviceRequestDTO addDeviceRequest,
			ClaimsPrincipal userClaims
		) => {
			var username = userClaims.Identity?.Name ?? "";
			var user = await dbContext.Users
				.Where(user => user.UserName == username)
				.FirstOrDefaultAsync();

			if (user is not User) {
				return Results.Json(new { message = $"Unable to load user by username {username}" }, statusCode: 401);
			}

			return Results.Ok();
		})
		.WithOpenApi()
		.WithName("RegisterNewDevice")
		.Produces<AddDeviceResponseDTO>()
		.RequireAuthorization(
			WaterQualityMonitorPolicies.IsUser,
			WaterQualityMonitorPolicies.ActiveSubscription
		);
	}
}