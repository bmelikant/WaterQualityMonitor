using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterQualityMonitorApi.Database.Models;

public class Device {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]	
	public int Id { get; set; }

	[MaxLength(36)]
	public string Guid { get; set; } = "";
	[MaxLength(255)]
	public string Secret { get; set; } = "";

	public virtual User Owner { get; set; } = null!;
	public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
}