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
    public class ProvinciaRepository : Repository<Provincia>, IProvinciaRepository
    {
        private readonly ApplicationDbContext _db;
        public ProvinciaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Provincia provincia)
        {
            if (provincia.Id == 0)
            {
                return _db.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia);
            }
            return _db.Provincias.Any(p => p.NombreProvincia == provincia.NombreProvincia && p.Id != provincia.Id);
        }


        public void Update(Provincia provincia)
        {
            _db.Provincias.Update(provincia);
        }
    }
}
