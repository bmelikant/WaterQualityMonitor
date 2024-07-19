namespace WaterQualityMonitorApi.Models.Authentication;

public static class PasswordHasher {

	public static string GeneratePasswordHash(string password, string saltValue) {
		// TODO: add an ACTUAL hashing mechanism here, huh?
		return $"{password}-{saltValue}";
	}
}