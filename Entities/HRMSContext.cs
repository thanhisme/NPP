using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class HRMSContext : DbContext
    {
        public HRMSContext() { }

        public HRMSContext(DbContextOptions<HRMSContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public virtual DbSet<BlackListToken> BlackListTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
