using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cuchillos2023.Models.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().HasData(
                    new Categoria { Id = 1, NombreCategoria = "Filetero"  },
                    new Categoria { Id = 2, NombreCategoria = "Trinchero" },
                    new Categoria { Id = 3, NombreCategoria = "Puntillero" },
                    new Categoria { Id = 4, NombreCategoria = "Chuletero" },
                    new Categoria { Id = 5, NombreCategoria = "Cocinero" }

                );
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Numero> Numeros { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Cuchillo> Cuchillos { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Ciudad> Ciudades { get; set; }


    }
}
