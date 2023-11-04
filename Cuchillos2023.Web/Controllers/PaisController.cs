using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuchillos2023.Web.Controllers
{
    public class PaisController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public PaisController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var paisList = _uniOfWork.Paises.GetAll();
            return View(paisList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return View(pais);
            }
            if (_uniOfWork.Paises.Exists(pais))
            {
                ModelState.AddModelError(string.Empty, "¡El país ya existe!");
                return View(pais);
            }
            _uniOfWork.Paises.Add(pais);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro agregado exitosamente!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var pais = _uniOfWork.Paises.Get(p => p.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Pais pais)
        {
            if (!ModelState.IsValid)
            {
                return View(pais);
            }
            if (_uniOfWork.Paises.Exists(pais))
            {
                ModelState.AddModelError(string.Empty, "¡El país ya existe!");
                return View(pais);
            }
            _uniOfWork.Paises.Update(pais);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro actualizado exitosamente!";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var pais = _uniOfWork.Paises.Get(p => p.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var pais = _uniOfWork.Paises.Get(p => p.Id == id);
            if (pais == null)
            {
                ModelState.AddModelError(string.Empty, "¡El país no existe!");
            }
            _uniOfWork.Paises.Delete(pais);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }

    }
}
