using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;
using WaterQualityMonitorApi.Models.Authentication;
using WaterQualityMonitorApi.Models.Constants;

namespace WaterQualityMonitorApi.Models.Permissions;

public static class Permissions {
	public const string API_Access = "API_ACCESS";
	public const string API_AdminRead = "API_READ_ADMIN";
	public const string API_AdminWrite = "API_WRITE_ADMIN";
}

public static class PermissionsPolicyExtensions {
	public static void AddPermissions(this AuthorizationOptions options) {
		options.AddPolicy(Permissions.API_Access, policy => policy.AddRequirements(new PermissionRequirement(Permissions.API_Access)));
		options.AddPolicy(Permissions.API_AdminRead, policy => policy.AddRequirements(new PermissionRequirement(Permissions.API_AdminRead)));
		options.AddPolicy(Permissions.API_AdminWrite, policy => policy.AddRequirements(new PermissionRequirement(Permissions.API_AdminWrite)));
	}
}

public class PermissionRequirement : IAuthorizationRequirement {
	public string PermissionName { get; set; } = "";
	public PermissionRequirement(string permissionName) => PermissionName = permissionName;
}

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {

	private readonly WaterMonitorDbContext _dbContext;
	public PermissionRequirementHandler(WaterMonitorDbContext dbContext) {
		_dbContext = dbContext;
	}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task HandleRequirementAsync(
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        AuthorizationHandlerContext context,
		PermissionRequirement permissionRequirement
	) {
		
		var username = context.User?.Identity?.Name ?? "";
		if (string.IsNullOrWhiteSpace(username)) throw new Exception("Could not determine username");
		var user = await _dbContext.Users
			.Include(user => user.Permissions)
			.Where(user => user.UserName == username)
			.FirstOrDefaultAsync();

		if (user is null) throw new Exception($"Unable to locate user {username}");

		if (user.Permissions
			.Select(p => p.PermissionName)
			.Contains(permissionRequirement.PermissionName)
		) {
			context.Succeed(permissionRequirement);
		}
	}
}