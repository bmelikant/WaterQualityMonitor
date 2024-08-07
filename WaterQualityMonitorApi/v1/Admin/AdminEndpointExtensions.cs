using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;
using WaterQualityMonitorApi.Database.Models;
using WaterQualityMonitorApi.Models.Authentication;
using WaterQualityMonitorApi.Models.Permissions;
using WaterQualityMonitorApi.V1.Admin.Models;

namespace WaterQualityMonitorApi.V1.Admin;

public static class AdminEndpointExtensions {
	public static RouteGroupBuilder AddAdminEndpoints(this RouteGroupBuilder routeGroupBuilder) {

		var adminRouteGroup = routeGroupBuilder.MapGroup("/admin")
			.WithTags("Administrative")
			.RequireAuthorization(policy => policy.RequirePermission(Permissions.API_Access));

		adminRouteGroup.MapGet("/users", async (
			[FromServices] WaterMonitorDbContext dbContext
		) => {

			var usersList = await dbContext.Users
				.Select(user => new AddUserResponseDTO {
					Id = user.Id,
					UserName = user.UserName
				}).ToListAsync();

			return Results.Json(usersList);
		})
		.WithOpenApi()
		.WithName("GetUsersList")
		.Produces<IEnumerable<AddUserResponseDTO>>();

		adminRouteGroup.MapPost("/users/add", async (
			[FromServices] WaterMonitorDbContext dbContext,
			[FromBody] AddUserRequestDTO addUserDto
		) => {

			var user = new User() {
				UserName = addUserDto.UserName,
				EmailAddress = addUserDto.EmailAddress,
				// TODO: add an ACTUAL salt value generator here, huh?
				Password = PasswordHasher.GeneratePasswordHash(addUserDto.Password, "salt"),
				ActiveUser = addUserDto.IsActive
			};

			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();

			return Results.Created(
				$"/users/{user.Id}",
				new AddUserResponseDTO() {
					Id = user.Id,
					UserName = user.UserName
				}
			);
		})
		.WithOpenApi()
		.WithName("AddNewUser")
		.Produces<AddUserResponseDTO>(statusCode: 201)
		.RequireAuthorization(policy => policy.RequirePermission(Permissions.API_AdminWrite));

		return routeGroupBuilder;
	}
}