using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NumeroController : Controller
    {

        private readonly IUnitOfWork _uniOfWork;

        public NumeroController(IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var numeroList = _uniOfWork.Numeros.GetAll();
            return View(numeroList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Numero numero)
        {
            if (!ModelState.IsValid)
            {
                return View(numero);
            }
            if (_uniOfWork.Numeros.Exists(numero))
            {
                ModelState.AddModelError(string.Empty, "¡El número ya existe!");
                return View(numero);
            }
            _uniOfWork.Numeros.Add(numero);
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
            var numero = _uniOfWork.Numeros.Get(n => n.Id == id);
            if (numero == null)
            {
                return NotFound();
            }
            return View(numero);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Numero numero)
        {
            if (!ModelState.IsValid)
            {
                return View(numero);
            }
            if (_uniOfWork.Numeros.Exists(numero))
            {
                ModelState.AddModelError(string.Empty, "¡El número ya existe!");
                return View(numero);
            }
            _uniOfWork.Numeros.Update(numero);
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
            var numero = _uniOfWork.Numeros.Get(n => n.Id == id);
            if (numero == null)
            {
                return NotFound();
            }
            return View(numero);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var numero = _uniOfWork.Numeros.Get(n => n.Id == id);
            if (numero == null)
            {
                ModelState.AddModelError(string.Empty, "¡El número no existe!");
            }
            _uniOfWork.Numeros.Delete(numero);
            _uniOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }



    }
}
