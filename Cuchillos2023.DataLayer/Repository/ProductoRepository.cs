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
    public class ProductoRepository : Repository<Producto>, IProductoRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Producto producto)
        {
            if (producto.Id == 0)
            {
                return _db.Productos.Any(c => c.NumeroId == producto.NumeroId && c.CuchilloId==producto.CuchilloId);
            }
            return _db.Productos.Any(c => c.NumeroId == producto.NumeroId && c.CuchilloId == producto.CuchilloId && c.Id != producto.Id);
        }


        public void Update(Producto producto)
        {
            _db.Productos.Update(producto);
        }
    }
}
