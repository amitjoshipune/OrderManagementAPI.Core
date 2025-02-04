using CommonServicesLib.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class SqlServerDbContext :DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasIndex(u => u.LoginId)
                .IsUnique();
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
