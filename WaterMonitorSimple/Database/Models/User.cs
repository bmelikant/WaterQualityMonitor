using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterMonitorSimple.Database.Models;

public class User {
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string UserName { get; set; } = null!;
	public string Secret { get; set; } = null!;

	public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}