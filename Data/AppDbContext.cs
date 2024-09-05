using LocaWebee.Models;
using Microsoft.EntityFrameworkCore;

namespace LocaWebee.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseInMemoryDatabase("locawebee");
            
    }
}
