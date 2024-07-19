using System.Text.Json.Serialization;

namespace WaterQualityMonitorApi.V1.Admin.Models;

public class AddUserResponseDTO {
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("userName")]
	public string UserName { get; set; } = "";
}