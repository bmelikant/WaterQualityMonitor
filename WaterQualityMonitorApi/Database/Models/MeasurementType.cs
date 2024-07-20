using System.ComponentModel.DataAnnotations.Schema;

namespace WaterQualityMonitorApi.Database.Models;

public class MeasurementType {
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Name { get; set; } = "";
}