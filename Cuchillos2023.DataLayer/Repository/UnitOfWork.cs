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

        }
        public ICategoriaRepository Categorias { get; private set; }
        public IMaterialRepository Materiales { get; private set; }
        public INumeroRepository Numeros { get; private set; }
        public IPaisRepository Paises { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
