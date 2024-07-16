using Microsoft.AspNetCore.Authorization;
using WaterQualityMonitorApi.Models.Authentication;
using WaterQualityMonitorApi.Models.Constants;

namespace WaterQualityMonitorApi.Models.Permissions;

public static class Permissions {
	public const string API_Access = "API_ACCESS";
}

public static class PermissionsPolicyExtensions {
	public static void AddPermissions(this AuthorizationOptions options) {
		options.AddPolicy(Permissions.API_Access, policy => policy.AddRequirements(new PermissionRequirement(Permissions.API_Access)));
	}
}

public class PermissionRequirement : IAuthorizationRequirement {
	public string PermissionName { get; set; } = "";
	public PermissionRequirement(string permissionName) => PermissionName = permissionName;
}

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task HandleRequirementAsync(
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        AuthorizationHandlerContext context,
		PermissionRequirement permissionRequirement
	) {
		
		var username = context.User?.Identity?.Name ?? "";
		if (string.IsNullOrWhiteSpace(username)) throw new Exception("Could not determine username");
		var user = UserRepository.FindWithoutPassword(username);

		if (user is null) throw new Exception($"Unable to locate user {username}");

		if (user.Permissions.Contains(permissionRequirement.PermissionName)) {
			context.Succeed(permissionRequirement);
		}
	}
}