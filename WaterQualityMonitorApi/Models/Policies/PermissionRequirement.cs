using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database;

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

public class PermissionsOneOfRequirement : IAuthorizationRequirement {
	public string [] Permissions { get; set; } = Array.Empty<string>();

	public PermissionsOneOfRequirement(params string [] permissions) {
		Permissions = permissions;
	}
}

public class PermissionsAllRequirement : IAuthorizationRequirement {
	public string [] Permissions { get; set; } = Array.Empty<string>();
	
	public PermissionsAllRequirement(params string [] permissions) {
		Permissions = permissions;
	}
}

public static class PermissionPolicyBuilderExtensions {
	public static AuthorizationPolicyBuilder RequireOneOf(this AuthorizationPolicyBuilder builder, params string [] permissions) {
		builder.AddRequirements(new PermissionsOneOfRequirement(permissions));
		return builder;
	}

	public static AuthorizationPolicyBuilder RequireAllOf(this AuthorizationPolicyBuilder builder, params string [] permissions) {
		builder.AddRequirements(new PermissionsAllRequirement(permissions));
		return builder;
	}

	public static AuthorizationPolicyBuilder RequirePermission(this AuthorizationPolicyBuilder builder, string permissionName) {
		builder.AddRequirements(new PermissionRequirement(permissionName));
		return builder;
	}
}

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {

	private readonly WaterMonitorDbContext _dbContext;
	public PermissionRequirementHandler(WaterMonitorDbContext dbContext) {
		_dbContext = dbContext;
	}

    protected override async Task HandleRequirementAsync(
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

public class RequireOneOfPermissionRequirementHandler : AuthorizationHandler<PermissionsOneOfRequirement> {
	private readonly WaterMonitorDbContext _dbContext;

	public RequireOneOfPermissionRequirementHandler(WaterMonitorDbContext dbContext) 
		=> _dbContext = dbContext;

	protected override async Task HandleRequirementAsync(
		AuthorizationHandlerContext context, 
		PermissionsOneOfRequirement requirement
	) {
		var username = context.User?.Identity?.Name ?? "";
		if (string.IsNullOrWhiteSpace(username)) throw new Exception("Could not determine username");
		var user = await _dbContext.Users
			.Include(user => user.Permissions)
			.Where(user => user.UserName == username)
			.FirstOrDefaultAsync();

		if (user is null) throw new Exception($"Unable to locate user {username}");

		foreach (var permission in requirement.Permissions) {
			if (user.Permissions.Select(p => p.PermissionName).Contains(permission)) {
				context.Succeed(requirement);
			}
		}
	}
}

public class RequireAllOfPermissionRequirementHandler : AuthorizationHandler<PermissionsOneOfRequirement> {
	private readonly WaterMonitorDbContext _dbContext;

	public RequireAllOfPermissionRequirementHandler(WaterMonitorDbContext dbContext) 
		=> _dbContext = dbContext;

	protected override async Task HandleRequirementAsync(
		AuthorizationHandlerContext context, 
		PermissionsOneOfRequirement requirement
	) {
		var username = context.User?.Identity?.Name ?? "";
		if (string.IsNullOrWhiteSpace(username)) throw new Exception("Could not determine username");
		var user = await _dbContext.Users
			.Include(user => user.Permissions)
			.Where(user => user.UserName == username)
			.FirstOrDefaultAsync();

		if (user is null) throw new Exception($"Unable to locate user {username}");

		foreach (var permission in requirement.Permissions) {
			if (!user.Permissions.Select(p => p.PermissionName).Contains(permission)) {
				return;
			}
		}

		context.Succeed(requirement);
	}
}