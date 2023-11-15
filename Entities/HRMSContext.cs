using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class HRMSContext : DbContext
    {
        public HRMSContext() { }

        public HRMSContext(DbContextOptions<HRMSContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<BlackListToken> BlackListTokens { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
                .HasMany(e => e.DefaultPermissions)
                .WithMany(e => e.Positions);
        }
    }
}
