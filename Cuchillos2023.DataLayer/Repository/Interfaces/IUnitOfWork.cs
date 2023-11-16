using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.DataLayer.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoriaRepository Categorias { get; }
        IMaterialRepository Materiales { get; }
        INumeroRepository Numeros { get; }
        IPaisRepository Paises { get; }
        ICuchillolRepository Cuchillos { get; }
        IProvinciaRepository Provincias { get; }
        ICiudadRepository Ciudades { get; }

        void Save();
    }
}
