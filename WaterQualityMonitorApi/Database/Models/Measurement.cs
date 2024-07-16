using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterQualityMonitorApi.Database.Models;

public class Measurement {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public DateTime MeasuredTime { get; set; }
	public double Value { get; set; }

	public virtual Device Device { get; set; } = null!;
}