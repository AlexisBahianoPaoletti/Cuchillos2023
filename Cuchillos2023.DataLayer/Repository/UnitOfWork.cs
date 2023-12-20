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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categorias = new CategoriaRepository(_db);
            Materiales = new MaterialRepository(_db);
            Numeros = new NumeroRepository(_db);
            Paises = new PaisRepository(_db);
            Cuchillos = new CuchilloRepository(_db);
            Provincias = new ProvinciaRepository(_db);
            Ciudades = new CiudadRepository(_db);
            Productos = new ProductoRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            OrderHeaders = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailRepository(_db);
            ApplicationUsers = new ApplicationUserRepository(_db);
        }

        public ICategoriaRepository Categorias { get; private set; }
        public IMaterialRepository Materiales { get; private set; }
        public INumeroRepository Numeros { get; private set; }
        public IPaisRepository Paises { get; private set; }
        public ICuchillolRepository Cuchillos { get; private set; }
        public IProvinciaRepository Provincias { get; private set; }
        public ICiudadRepository Ciudades { get; private set; }
        public IProductoRepository Productos { get; private set; }
        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IOrderHeaderRepository OrderHeaders { get; private set; }
        public IOrderDetailRepository OrderDetails { get; private set; }

        public IApplicationUserRepository ApplicationUsers { get; private set; }


        public void BeginTransaction()
        {
            //_db.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            //_db.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            //_db.Database.RollbackTransaction();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
