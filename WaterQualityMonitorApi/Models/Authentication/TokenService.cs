using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using WaterQualityMonitorApi.Models.Constants;

namespace WaterQualityMonitorApi.Models.Authentication;

internal class TokenService {

	private readonly ILogger<TokenService> _logger;
	private readonly JwtSecurityTokenHandler _tokenHandler = new();

	public TokenService(ILogger<TokenService> logger) => 
		_logger = logger;

	internal string GenerateToken(UserModel userModel) {
		var key = AuthenticationSettings.GenerateSecretByte();

		List<Claim> claims = new List<Claim> {
			new Claim(ClaimTypes.Name, userModel.UserName.ToString())
		};

		foreach (var role in userModel.Permissions) {
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		if (userModel.ActiveSubscription) {
			claims.Add(new Claim(WaterQualityMonitorClaimTypes.ActiveSubscription, "true"));
		}

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims.ToArray()),
			Expires = DateTime.UtcNow.AddMinutes(30),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = _tokenHandler.CreateToken(tokenDescriptor);
		return _tokenHandler.WriteToken(token);
	}

	internal string GenerateDeviceToken(string deviceGuid) {
		var key = AuthenticationSettings.GenerateSecretByte();

		List<Claim> deviceClaims = new List<Claim> {
			new Claim(ClaimTypes.SerialNumber, deviceGuid),
			new Claim(WaterQualityMonitorClaimTypes.DeviceLogin, "")
		};

		var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(deviceClaims.ToArray()),
			Expires = DateTime.UtcNow.AddMinutes(30),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = _tokenHandler.CreateToken(tokenDescriptor);
		return _tokenHandler.WriteToken(token);
	}
}