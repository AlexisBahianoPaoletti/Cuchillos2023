using Cuchillos2023.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Cuchillos2023.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            //modelBuilder.Entity<Categoria>().HasData(
            //        new Categoria { Id = 1, NombreCategoria = "Filetero", DisplayOrder = 1 },
            //        new Categoria { Id = 2, NombreCategoria = "Trinchero", DisplayOrder = 3 },
            //        new Categoria { Id = 3, NombreCategoria = "Puntillero", DisplayOrder = 2 },
            //        new Categoria { Id = 4, NombreCategoria = "Chuletero", DisplayOrder = 4 },
            //        new Categoria { Id = 5, NombreCategoria = "Cocinero", DisplayOrder = 5 }

            //    );
        //}

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Numero> Numeros { get; set; }
        public DbSet<Pais> Paises { get; set; }


    }
}
