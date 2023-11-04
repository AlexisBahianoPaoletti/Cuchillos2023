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
    public class PaisRepository : Repository<Pais>, IPaisRepository
    {
        private readonly ApplicationDbContext _db;
        public PaisRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Pais pais)
        {
            if (pais.Id == 0)
            {
                return _db.Paises.Any(p => p.NombrePais == pais.NombrePais);
            }
            return _db.Paises.Any(p => p.NombrePais == pais.NombrePais && p.Id != pais.Id);
        }


        public void Update(Pais pais)
        {
            _db.Paises.Update(pais);
        }
    }
}
