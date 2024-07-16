using Microsoft.AspNetCore.Mvc;
using WaterQualityMonitorApi.Models.Authentication;
using WaterQualityMonitorApi.V1.Login.Models;

namespace WaterQualityMonitorApi.V1.Login;

public static class LoginEndpointExtensions {

	public static RouteGroupBuilder AddLoginEndpoints(this RouteGroupBuilder routeGroup) {
		
		routeGroup.MapPost("/login/user", ([FromServices] ILoggerFactory loggerFactory, UserLoginRequestDTO loginDTO, TokenService tokenService) => {
			var logger = loggerFactory.CreateLogger(typeof(LoginEndpointExtensions));

			logger.LogInformation($"Attempting user login for user {loginDTO.UserName}");
			var user = UserRepository.Find(loginDTO.UserName, loginDTO.Password);

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

		routeGroup.MapPost("/login/device", (

		) => {

		})
		.WithOpenApi()
		.Produces<DeviceLoginResponseDTO>();

		return routeGroup;
	}
}