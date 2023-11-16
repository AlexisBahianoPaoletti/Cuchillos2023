using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CiudadController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public CiudadController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var ciudadList = _uniOfWork.Ciudades.GetAll();
            List<CiudadListVm> ciudadListVm = new List<CiudadListVm>();
            var provinciasList = _uniOfWork.Provincias.GetAll();
            foreach (var ciudad in ciudadList)
            {
                var ciudadVm = new CiudadListVm()
                {
                    Id = ciudad.Id,
                    NombreCiudad = ciudad.NombreCiudad,
                };
                foreach (var provincia in provinciasList)
                {
                    if (ciudad.ProvinciaId == provincia.Id)
                    {
                        ciudadVm.NombreProvincia = provincia.NombreProvincia;
                    }
                }

                ciudadListVm.Add(ciudadVm);

            }
            return View(ciudadListVm);
        }

        
        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            var ciudadVm = new CiudadEditVm
            {
                Ciudad = new Ciudad(),
                ProvinciasList = _uniOfWork.Provincias.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreProvincia,
                        Value = c.Id.ToString()
                    })

            };

            if (id == null || id == 0)
            {
                return View(ciudadVm);

            }
            else
            {
                ciudadVm.Ciudad = _uniOfWork.Ciudades.Get(c => c.Id == id.Value);

                return View(ciudadVm);

            }
        }
        [HttpPost]
        public IActionResult Upsert(CiudadEditVm ciudadVm)
        {
            if (!ModelState.IsValid)
            {
                ciudadVm.ProvinciasList = _uniOfWork.Provincias
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreProvincia,
                        Value = c.Id.ToString()
                    });

                return View(ciudadVm);
            }
            if (ciudadVm.Ciudad.Id == 0)
            {
                _uniOfWork.Ciudades.Add(ciudadVm.Ciudad);
                TempData["success"] = "¡Registro agregado exitosamente!";
            }
            else
            {
                _uniOfWork.Ciudades.Update(ciudadVm.Ciudad);
                TempData["success"] = "¡Registro actualizado exitosamente!";
            }
            _uniOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var ciudad = _uniOfWork.Ciudades.Get(c => c.Id == id);
            if (ciudad == null)
            {
                return NotFound();
            }
            return View(ciudad);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var ciudad = _uniOfWork.Ciudades.Get(c => c.Id == id);
            if (ciudad == null)
            {
                ModelState.AddModelError(string.Empty, "¡La ciudad no existe!");
            }
            _uniOfWork.Ciudades.Delete(ciudad);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }
    }
}
