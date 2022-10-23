using IntelRobotics.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelRobotics.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Robot> Robots { get; set; }
        public DbSet<KontaktForm> kontaktForms { get; set; }
        public DbSet<KontaktFormToRobot> kontaktFormToRobots { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<KontaktFormToRobot>().HasKey(ir => new { ir.KontaktFormId, ir.RobotId });
        }
    }
}