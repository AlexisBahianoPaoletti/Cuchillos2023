﻿using Cuchillos2023.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.DataLayer.Repository.Interfaces
{
    public interface IPaisRepository:IRepository<Pais>
    {
        void Update(Pais pais);
        bool Exists(Pais pais);
    }
}
