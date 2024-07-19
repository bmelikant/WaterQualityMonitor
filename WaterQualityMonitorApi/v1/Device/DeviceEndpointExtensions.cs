using Microsoft.AspNetCore.Mvc;
using WaterQualityMonitorApi.Models.Constants;
using WaterQualityMonitorApi.V1.Device.Models;

namespace WaterQualityMonitorApi.V1.Device;

public static class DeviceEndpointExtensions {

	public static void AddDeviceEndpoints(this RouteGroupBuilder routeGroupBuilder) {

		routeGroupBuilder.MapPost("/device/status", (

		) => {
			
		});

		routeGroupBuilder.MapPost("/device/register", (
			[FromBody] AddDeviceRequestDTO addDeviceRequest
		) => {
			
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