using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Data;
using Cuchillos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.DataLayer.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                return _db.Categorias.Any(c => c.NombreCategoria == categoria.NombreCategoria);
            }
            return _db.Categorias.Any(c => c.NombreCategoria == categoria.NombreCategoria && c.Id != categoria.Id);
        }


        public void Update(Categoria categoria)
        {
            _db.Categorias.Update(categoria);
        }
    }
}
