using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuchillos2023.Web.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IUnitOfWork _uniOfWork;
        public MaterialController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var materialList = _uniOfWork.Materiales.GetAll();
            return View(materialList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Material material)
        {
            if (!ModelState.IsValid)
            {
                return View(material);
            }
            if (_uniOfWork.Materiales.Exists(material))
            {
                ModelState.AddModelError(string.Empty, "¡El material ya existe!");
                return View(material);
            }
            _uniOfWork.Materiales.Add(material);
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
            var material = _uniOfWork.Materiales.Get(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Material material)
        {
            if (!ModelState.IsValid)
            {
                return View(material);
            }
            if (_uniOfWork.Materiales.Exists(material))
            {
                ModelState.AddModelError(string.Empty, "¡El material ya existe!");
                return View(material);
            }
            _uniOfWork.Materiales.Update(material);
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
            var material = _uniOfWork.Materiales.Get(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }
            return View(material);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var material = _uniOfWork.Materiales.Get(m => m.Id == id);
            if (material == null)
            {
                ModelState.AddModelError(string.Empty, "¡El material no existe!");
            }
            _uniOfWork.Materiales.Delete(material);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }



    }
}
