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
    public class NumeroRepository : Repository<Numero>, INumeroRepository
    {
        private readonly ApplicationDbContext _db;
        public NumeroRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Numero numero)
        {
            if (numero.Id == 0)
            {
                return _db.Numeros.Any(n => n.Numeroo == numero.Numeroo);
            }
            return _db.Numeros.Any(n => n.Numeroo == numero.Numeroo && n.Id != numero.Id);
        }


        public void Update(Numero numero)
        {
            _db.Numeros.Update(numero);
        }
    }
}
