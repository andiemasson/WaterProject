using Microsoft.EntityFrameworkCore;

namespace WaterProject.API.Data;

public class WaterProjectContext : DbContext
{
    public WaterProjectContext(DbContextOptions<WaterProjectContext> options) : base(options)
    {
    }
    
    public DbSet<Project> Projects { get; set; }
}