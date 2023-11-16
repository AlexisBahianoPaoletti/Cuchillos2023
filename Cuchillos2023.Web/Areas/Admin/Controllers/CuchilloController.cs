using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.Metadata;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CuchilloController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CuchilloController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            var cuchilloVm = new CuchilloEditVm
            {
                Cuchillo = new Cuchillo(),
                CategoriasList = _unitOfWork.Categorias
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCategoria,
                        Value = c.Id.ToString()
                    }),
                MaterialesList = _unitOfWork.Materiales
                    .GetAll()
                    .Select(m => new SelectListItem
                    {
                        Text = m.NombreMaterial,
                        Value = m.Id.ToString()
                    })
            };

            if (id == null || id == 0)
            {
                return View(cuchilloVm);

            }
            else
            {
                var wwwRootPath = _webHostEnvironment.ContentRootPath;
                cuchilloVm.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == id.Value);
                if (cuchilloVm.Cuchillo.ImagenUrl != null)
                {
                    var oldImage = Path.Combine(wwwRootPath, cuchilloVm.Cuchillo.ImagenUrl.TrimStart('\\'));
                    if (!System.IO.File.Exists(oldImage))
                    {
                        //var noExiste = Path.Combine(wwwRootPath, @imagenes\SinImagenDisponible.jpg");
                        var noExiste = @"\imagenes\SinImagenDisponible.jpg";
                        cuchilloVm.Cuchillo.ImagenUrl = noExiste;
                    }
                }
                return View(cuchilloVm);

            }
        }

        [HttpPost]
        public IActionResult Upsert(CuchilloEditVm cuchilloVm, IFormFile? file)
        {
            if (!ModelState.IsValid)
            {
                cuchilloVm.CategoriasList = _unitOfWork.Categorias 
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCategoria,
                        Value = c.Id.ToString()
                    });
                cuchilloVm.MaterialesList = _unitOfWork.Materiales
                    .GetAll()
                    .Select(m => new SelectListItem
                    {
                        Text = m.NombreMaterial,
                        Value = m.Id.ToString()
                    });

                return View(cuchilloVm);
            }
            if (file != null)
            {
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);

                if (cuchilloVm.Cuchillo.ImagenUrl != null)
                {
                    var oldImage = Path.Combine(wwwRootPath, cuchilloVm.Cuchillo.ImagenUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }


                var uploadFolder = Path.Combine(wwwRootPath, @"imagenes\cuchillos\");
                using (var fileStream = new FileStream(Path.Combine(
                    uploadFolder, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                cuchilloVm.Cuchillo.ImagenUrl = @"\imagenes\cuchillos\" + fileName + extension;
            }
            if (cuchilloVm.Cuchillo.Id == 0)
            {
                _unitOfWork.Cuchillos.Add(cuchilloVm.Cuchillo);
                TempData["success"] = "¡Registro agregado exitosamente!";
            }
            else
            {
                _unitOfWork.Cuchillos.Update(cuchilloVm.Cuchillo);
                TempData["success"] = "¡Registro actualizado exitosamente!";
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var cuchilloList = _unitOfWork.Cuchillos.GetAll();
            List<CuchilloListVm> cuchilloListVm = new List<CuchilloListVm>();
            foreach (var cuchillo in cuchilloList)
            {
                var cuchilloVm = new CuchilloListVm()
                {
                    Id = cuchillo.Id,
                    NombreCuchillo = cuchillo.NombreCuchillo,
                    Precio = cuchillo.Precio,
                    
                };
                cuchilloListVm.Add(cuchilloVm);

            }
            return Json(new { data = cuchilloListVm });

        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return Json(new { success = false, message = "No encontrado" });

            }
            var cuchilloDelete = _unitOfWork.Cuchillos.Get(p => p.Id == id);
            if (cuchilloDelete == null)
            {
                return Json(new { success = false, message = "Cuchillo no encontrado" });
            }
            try
            {
                _unitOfWork.Cuchillos.Delete(cuchilloDelete);
                _unitOfWork.Save();
                var wwwRootPath = _webHostEnvironment.ContentRootPath;
                if (cuchilloDelete.ImagenUrl != null)
                {
                    var imageToDelete = Path
                        .Combine(wwwRootPath, cuchilloDelete.ImagenUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imageToDelete))
                    {
                        System.IO.File.Delete(imageToDelete);
                    }
                }
                return Json(new { success = true, message = "¡Registro eliminado exitosamente!" });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion


    }
}
