using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuchillos2023.Web.Controllers
{
    public class CategoriaController : Controller
    {

        private readonly IUnitOfWork _uniOfWork;

        public CategoriaController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var categoryiaList = _uniOfWork.Categorias.GetAll();
            return View(categoryiaList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            if (_uniOfWork.Categorias.Exists(categoria))
            {
                ModelState.AddModelError(string.Empty, "¡La categoría ya existe!");
                return View(categoria);
            }
            _uniOfWork.Categorias.Add(categoria);
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
            var categoria = _uniOfWork.Categorias.Get(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            if (_uniOfWork.Categorias.Exists(categoria))
            {
                ModelState.AddModelError(string.Empty, "¡La categoría ya existe!");
                return View(categoria);
            }
            _uniOfWork.Categorias.Update(categoria);
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
            var categoria = _uniOfWork.Categorias.Get(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var categoria = _uniOfWork.Categorias.Get(c => c.Id == id);
            if (categoria == null)
            {
                ModelState.AddModelError(string.Empty, "¡La categoría no existe!");
            }
            _uniOfWork.Categorias.Delete(categoria);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }



    }
}
