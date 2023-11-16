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
    public class CuchilloRepository : Repository<Cuchillo>, ICuchillolRepository
    {
        private readonly ApplicationDbContext _db;
        public CuchilloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public bool Exists(Cuchillo cuchillo)
        {
            if (cuchillo.Id == 0)
            {
                return _db.Cuchillos.Any(c => c.NombreCuchillo == cuchillo.NombreCuchillo);
            }
            return _db.Cuchillos.Any(c => c.NombreCuchillo == cuchillo.NombreCuchillo
            && c.Id != cuchillo.Id);
        }

        public void Update(Cuchillo cuchillo)
        {
            _db.Cuchillos.Update(cuchillo);
        }

    }
}
