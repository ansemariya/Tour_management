using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Tour_management.Models;

namespace Tour_management.Data
{
    public class DbCon : DbContext
    {
        public DbCon(DbContextOptions<DbCon> option) : base(option) { }

        public DbSet<Package> Package { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<User> User { get;  set; }
    }

    
}
