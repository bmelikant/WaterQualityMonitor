using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WaterQualityMonitorApi.Database.Models;

public class User {
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set;}
	[MaxLength(100)]
	public string UserName { get; set; } = "";
	[MaxLength(255)]
	public string Password { get; set; } = "";
	[MaxLength(320)]
	public string EmailAddress { get; set; } = "";

	public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}