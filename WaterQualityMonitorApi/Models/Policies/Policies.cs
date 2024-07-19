using Microsoft.AspNetCore.Authorization;
using WaterQualityMonitorApi.Models.Constants;

namespace WaterQualityMonitorApi.Models.Policies;

public static class PolicyExtensions {
	public static void AddActiveSubscriptionPolicy(this AuthorizationOptions options) {
		options.AddPolicy(
			WaterQualityMonitorPolicies.ActiveSubscription, 
			policy => policy.RequireClaim(WaterQualityMonitorClaimTypes.ActiveSubscription, "true")
		);
	}

	public static void AddUserPolicy(this AuthorizationOptions options) {
		options.AddPolicy(
			WaterQualityMonitorPolicies.IsUser,
			policy => policy.RequireClaim(WaterQualityMonitorClaimTypes.UserLogin, "true")
		);
	}

	public static void AddDevicePolicy(this AuthorizationOptions options) {
		options.AddPolicy(
			WaterQualityMonitorPolicies.IsDevice,
			policy => policy.RequireClaim(WaterQualityMonitorClaimTypes.DeviceLogin, "true")
		);
	}
}