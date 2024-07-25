using Microsoft.EntityFrameworkCore;
using WaterMonitorSimple.Database.Models;

namespace WaterMonitorSimple.Database;

public class WaterMonitorDbContext : DbContext {
	public WaterMonitorDbContext(DbContextOptions<WaterMonitorDbContext> options) : base(options) {}
	
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Permission> Permissions { get; set; }
	public virtual DbSet<Measurement> Measurements { get; set; }
}