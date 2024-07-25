using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;
using WaterQualityMonitorApi.Models.Authentication;
using WaterQualityMonitorApi.V1.Login.Models;

namespace WaterQualityMonitorApi.V1.Login;

public static class LoginEndpointExtensions {

	public static RouteGroupBuilder AddLoginEndpoints(this RouteGroupBuilder routeGroup) {
		
		var loginRouteGroup = routeGroup.MapGroup("/login")
			.WithTags("Login")
			.WithOrder(0);

		loginRouteGroup.MapPost("/user", async (
			[FromServices] ILoggerFactory loggerFactory,
			[FromServices] WaterMonitorDbContext dbContext,
			UserLoginRequestDTO loginDTO, 
			TokenService tokenService
		) => {
			var logger = loggerFactory.CreateLogger(typeof(LoginEndpointExtensions));

			logger.LogInformation($"Attempting user login for user {loginDTO.UserName}");
			var user = await dbContext.Users
				.Where(user => user.UserName == loginDTO.UserName)
				.FirstOrDefaultAsync();

			if (user is null) {
				logger.LogWarning($"Could not find requested user {loginDTO.UserName}");
				return Results.Unauthorized();
			}

			logger.LogInformation($"Creating JWT bearer token for {user.UserName}");
			var token = tokenService.GenerateToken(user);
			user.Password = string.Empty;

			logger.LogInformation($"Logged in user {user.UserName}");
			return Results.Ok(new UserLoginResponseDTO { UserName = user.UserName, Token = token });
		})
		.WithOpenApi()
		.Produces<UserLoginResponseDTO>()
		.WithName("UserLogin");

		loginRouteGroup.MapPost("/device", (

		) => {

		})
		.WithOpenApi()
		.Produces<DeviceLoginResponseDTO>()
		.WithName("DeviceLogin");

		return routeGroup;
	}
}