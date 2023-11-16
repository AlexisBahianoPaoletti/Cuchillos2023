using Cuchillos2023.DataLayer.Repository;
using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvinciaController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public ProvinciaController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var provinciaList = _uniOfWork.Provincias.GetAll();
            List<ProvinciaListVm> provinciaListVm = new List<ProvinciaListVm>();
            var paisesList = _uniOfWork.Paises.GetAll();
            foreach (var provincia in provinciaList)
            {
                var provinciaVm = new ProvinciaListVm()
                {
                    Id = provincia.Id,
                    NombreProvincia = provincia.NombreProvincia,
                };
                foreach (var pais in paisesList)
                {
                    if (provincia.PaisId == pais.Id)
                    {
                        provinciaVm.NombrePais = pais.NombrePais;
                    }
                }

                provinciaListVm.Add(provinciaVm);

            }
            return View(provinciaListVm);
        }


        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            var provinciaVm = new ProvinciaEditVm
            {
                Provincia = new Provincia(),
                PaisesList = _uniOfWork.Paises.GetAll()
                    .Select(p => new SelectListItem
                    {
                        Text = p.NombrePais,
                        Value = p.Id.ToString()
                    })

            };

            if (id == null || id == 0)
            {
                return View(provinciaVm);

            }
            else
            {
                provinciaVm.Provincia = _uniOfWork.Provincias.Get(p => p.Id == id.Value);

                return View(provinciaVm);

            }
        }
        [HttpPost]
        public IActionResult Upsert(ProvinciaEditVm provinciaVm)
        {
            if (!ModelState.IsValid)
            {
                provinciaVm.PaisesList = _uniOfWork.Paises
                    .GetAll()
                    .Select(p => new SelectListItem
                    {
                        Text = p.NombrePais,
                        Value = p.Id.ToString()
                    });

                return View(provinciaVm);
            }
            if (provinciaVm.Provincia.Id == 0)
            {
                _uniOfWork.Provincias.Add(provinciaVm.Provincia);
                TempData["success"] = "¡Registro agregado exitosamente!";
            }
            else
            {
                _uniOfWork.Provincias.Update(provinciaVm.Provincia);
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
            var provincia = _uniOfWork.Provincias.Get(p => p.Id == id);
            if (provincia == null)
            {
                return NotFound();
            }
            return View(provincia);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var provincia = _uniOfWork.Provincias.Get(p => p.Id == id);
            if (provincia == null)
            {
                ModelState.AddModelError(string.Empty, "¡La provincia no existe!");
            }
            _uniOfWork.Provincias.Delete(provincia);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }
    }
}
