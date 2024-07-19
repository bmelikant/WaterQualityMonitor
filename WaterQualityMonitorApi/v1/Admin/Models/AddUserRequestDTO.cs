using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WaterQualityMonitorApi.V1.Admin.Models;

public class AddUserRequestDTO {
	[JsonPropertyName("userName")]
	public string UserName { get; set; } = "";
	[JsonPropertyName("password")]
	public string Password { get; set; } = "";
	[JsonPropertyName("emailAddress")]
	public string EmailAddress { get; set; } = "";
	[JsonPropertyName("isActive")]
	public bool IsActive { get; set; } = false;
}