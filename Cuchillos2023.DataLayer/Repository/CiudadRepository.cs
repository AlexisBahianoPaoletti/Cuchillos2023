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
    public class CiudadRepository : Repository<Ciudad>, ICiudadRepository
    {
        private readonly ApplicationDbContext _db;
        public CiudadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Ciudad ciudad)
        {
            if (ciudad.Id == 0)
            {
                return _db.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad);
            }
            return _db.Ciudades.Any(c => c.NombreCiudad == ciudad.NombreCiudad && c.Id != ciudad.Id);
        }


        public void Update(Ciudad ciudad)
        {
            _db.Ciudades.Update(ciudad);
        }
    }
}
