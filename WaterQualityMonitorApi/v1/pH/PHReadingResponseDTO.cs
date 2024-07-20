namespace WaterQualityMonitorApi.V1.PH;

public class PHReadingResponseDTO {
	public string DeviceGuid { get; set; } = "";
	public DateTime? ReadingDateAndTime { get; set; }
	public string Value { get; set; } = "";
}