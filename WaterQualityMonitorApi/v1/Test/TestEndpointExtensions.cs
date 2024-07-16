using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WaterQualityMonitorApi.Models.Permissions;

namespace WaterQualityMonitorApi.V1.Test;

public static class TestEndpointExtensions {

	public static RouteGroupBuilder AddTestEndpoints(this RouteGroupBuilder routeGroup) {
		
		var testGroup = routeGroup.MapGroup("/test")
			.RequireAuthorization(Permissions.API_Access);

		testGroup.MapGet("/test-role-one", (
			[FromServices] ILoggerFactory loggerFactory,
			ClaimsPrincipal user
		) => {
			return Results.Ok(new { message = $"Authenticated with role MyRole as user {user?.Identity?.Name}" });
		})
		.WithOpenApi()
		.RequireAuthorization("MyRole");

		testGroup.MapGet("/test-role-two", (ClaimsPrincipal user) => {
			var claimTypes = user.Claims.Select(claim => claim.Type).ToArray();
			return Results.Ok(new TestRoleTwoResponseDTO { UserName = user?.Identity?.Name ?? "", ClaimTypes = claimTypes});
		})
		.WithOpenApi()
		.RequireAuthorization(
			"MyOtherRole",
			"RequireSubscription"
		)
		.Produces<TestRoleTwoResponseDTO>();

		return routeGroup;
	}
}

class TestRoleTwoResponseDTO {
	public string UserName { get; set; } = "";
	public string [] ClaimTypes { get; set; } = Array.Empty<string>();
}