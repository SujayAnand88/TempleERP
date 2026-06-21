using Microsoft.EntityFrameworkCore;
using TempleERP.Models;

namespace TempleERP.Data
{
    public class TempleDbContext : DbContext
    {
        public TempleDbContext(DbContextOptions<TempleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<PoojaMaster> PoojaMaster { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Items> Items { get; set; } 
    }
}
