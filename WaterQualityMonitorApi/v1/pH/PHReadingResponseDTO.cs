namespace WaterQualityMonitorApi.V1.PH;

public class PHReadingResponseDTO {
	public DateTime? ReadingDateAndTime { get; set; }
	public string Value { get; set; } = "";
}