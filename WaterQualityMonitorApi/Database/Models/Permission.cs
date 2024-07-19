using System.ComponentModel.DataAnnotations;

namespace WaterQualityMonitorApi.Database.Models;

public class Permission {
	[Key]
	public int Id { get; set; }
	public string PermissionName { get; set; } = "";
	
	public virtual ICollection<User> Users { get; set; } = new List<User>();
}