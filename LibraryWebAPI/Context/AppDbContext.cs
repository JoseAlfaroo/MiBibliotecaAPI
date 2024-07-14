using LibraryWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base( options) { }
        
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<GeneroLibro> GenerosLibro { get; set; }
        public DbSet<AutorLibro> AutoresLibro { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurando Clave Compuesta LIBRO-GENERO
            modelBuilder.Entity<GeneroLibro>()
                .HasKey(gl => new { gl.LibroID, gl.GeneroID });

            // Configurando Clave Compuesta AUTOR-GENERO
            modelBuilder.Entity<AutorLibro>()
                .HasKey(al => new { al.LibroID, al.AutorID });

        }

    }
}
