
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterMonitorSimple.Database.Models;

public class Permission {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	public virtual ICollection<User> Users { get; set; } = new List<User>();
}