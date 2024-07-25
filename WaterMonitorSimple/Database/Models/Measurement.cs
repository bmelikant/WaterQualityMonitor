using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterMonitorSimple.Database.Models;

public class Measurement {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string DeviceName { get; set; } = null!;
	public string FirmwareRevision { get; set; } = null!;
	public string Address { get; set; } = null!;
	public double Value { get; set; }
	public DateTime MeasurementTime { get; set; }
}