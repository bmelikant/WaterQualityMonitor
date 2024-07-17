using Microsoft.EntityFrameworkCore;
using WaterQualityMonitorApi.Database.Models;

namespace WaterQualityMonitorApi.Database;

public class WaterMonitorDbContext : DbContext {

	public WaterMonitorDbContext(DbContextOptions<WaterMonitorDbContext> options) : base(options) {}
	
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Device> Devices { get; set; }
	public virtual DbSet<Measurement> Measurements { get; set; }
}