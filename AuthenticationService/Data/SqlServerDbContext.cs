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
    }
}
