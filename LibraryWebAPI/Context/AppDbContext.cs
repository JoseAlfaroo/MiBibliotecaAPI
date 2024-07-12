using LibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options) { }
        
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Autor> Autores { get; set; }
    }
}
