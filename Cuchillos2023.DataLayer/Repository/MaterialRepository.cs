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
    public class MaterialRepository : Repository<Material>, IMaterialRepository
    {
        private readonly ApplicationDbContext _db;
        public MaterialRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool Exists(Material material)
        {
            if (material.Id == 0)
            {
                return _db.Materiales.Any(m => m.NombreMaterial == material.NombreMaterial);
            }
            return _db.Materiales.Any(m => m.NombreMaterial == material.NombreMaterial && m.Id != material.Id);
        }


        public void Update(Material material)
        {
            _db.Materiales.Update(material);
        }
    }
}
