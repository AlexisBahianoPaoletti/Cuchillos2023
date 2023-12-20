using Cuchillos2023.DataLayer.Repository;
using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(IUnitOfWork unitOfWork/*, IWebHostEnvironment webHostEnvironment*/)
        {
            _unitOfWork = unitOfWork;
            //_webHostEnvironment = webHostEnvironment;
        }
		public IActionResult Index()
        {
			var productoList = _unitOfWork.Productos.GetAll();
			List<ProductoListVm> productoListVm = new List<ProductoListVm>();
			var cuchillosList = _unitOfWork.Cuchillos.GetAll();
			var numerosList = _unitOfWork.Numeros.GetAll();

			foreach (var producto in productoList)
			{
				var productoVm = new ProductoListVm()
				{
					Id = producto.Id,
                    Stock = producto.Stock,
                    Precio = producto.Cuchillo.Precio,
                    ImagenUrl = producto.Cuchillo.ImagenUrl
					
				};
				foreach (var cuchillo in cuchillosList)
				{
					if (producto.CuchilloId == cuchillo.Id)
					{
						productoVm.NombreCuchillo = cuchillo.NombreCuchillo;
					}
				}
				foreach (var numero in numerosList)
				{
					if (producto.NumeroId == numero.Id)
					{
						productoVm.Numero = numero.Numeroo;
					}
				}

				productoListVm.Add(productoVm);

			}
			return View(productoListVm);
		}

        [HttpGet]
        public IActionResult UpSert(int? id)
        {
            var productVm = new ProductoEditVm
            {
                Producto = new Producto(),
                CuchillosList = _unitOfWork.Cuchillos
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCuchillo,
                        Value = c.Id.ToString()
                    }),
                NumerosList = _unitOfWork.Numeros
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Numeroo.ToString(),
                        Value = c.Id.ToString()
                    })
            };

            if (id == null || id == 0)
            {
                return View(productVm);

            }
            else
            {
                //var wwwRootPath = _webHostEnvironment.WebRootPath;
                productVm.Producto = _unitOfWork.Productos.Get(p => p.Id == id.Value);
                //if (productVm.Product.ImageUrl != null)
                //{
                    //var oldImage = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
                    //if (!System.IO.File.Exists(oldImage))
                    //{
                        //var noExiste = Path.Combine(wwwRootPath, @images\SinImagenDisponible.jpg");
                        //var noExiste = @"\images\SinImagenDisponible.jpg";
                       //productVm.Product.ImageUrl = noExiste;
                    //}
                //}

                return View(productVm);

            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductoEditVm productVm/*, IFormFile? file*/)
        {
            if (!ModelState.IsValid)
            {
                productVm.CuchillosList = _unitOfWork.Cuchillos
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.NombreCuchillo,
                        Value = c.Id.ToString()
                    });
                productVm.NumerosList = _unitOfWork.Numeros
                    .GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Numeroo.ToString(),
                        Value = c.Id.ToString()
                    });

                return View(productVm);
            }
            //if (file != null)
            //{
            //    var wwwRootPath = _webHostEnvironment.WebRootPath;
            //    var fileName = Guid.NewGuid().ToString();
            //    var extension = Path.GetExtension(file.FileName);

            //    var productFile = fileName + extension;

            //    if (productVm.Product.ImageUrl != null)
            //    {
            //        var oldImage = Path.Combine(wwwRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
            //        if (System.IO.File.Exists(oldImage))
            //        {
            //            System.IO.File.Delete(oldImage);
            //        }
            //    }


            //    var uploadFolder = Path.Combine(wwwRootPath, @"images\products\");
            //    using (var fileStream = new FileStream(Path.Combine(uploadFolder, productFile), FileMode.Create))
            //    {
            //        file.CopyTo(fileStream);
            //    }
            //    productVm.Product.ImageUrl = @"\images\products\" + productFile;
            //}

            var p = _unitOfWork.Productos.Get(p => p.CuchilloId == productVm.Producto.CuchilloId && p.NumeroId == productVm.Producto.NumeroId);
            if (p == null)
            {
                if (productVm.Producto.Id == 0)
                {
                    _unitOfWork.Productos.Add(productVm.Producto);
                    TempData["success"] = "¡Registro agregado exitosamente!";
                }
                else
                {
                    _unitOfWork.Productos.Update(productVm.Producto);
                    TempData["success"] = "¡Registro actualizado exitosamente!";
                }
            }
            else
            {
                p.Stock += productVm.Producto.Stock;
                _unitOfWork.Productos.Update(p);
                TempData["success"] = "¡Se actualizo un Producto existente!";
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var producto = _unitOfWork.Productos.Get(c => c.Id == id, "Cuchillo,Numero");
            //producto.Cuchillo.Material = _unitOfWork.Materiales.Get(c => c.Id == producto.Cuchillo.MaterialId);
            //producto.Cuchillo.Categoria = _unitOfWork.Categorias.Get(c => c.Id == producto.Cuchillo.CategoriaId);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var producto = _unitOfWork.Productos.Get(c => c.Id == id);
            if (producto == null)
            {
                ModelState.AddModelError(string.Empty, "¡El producto no existe!");
            }
            _unitOfWork.Productos.Delete(producto);
            _unitOfWork.Save();
            TempData["success"] = "¡Registro eliminado exitosamente!";

            return RedirectToAction("Index");
        }

        //#region API CALL
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var productList = _unitOfWork.Productos.GetAll();
        //    List<ProductoListVm> productListVm = new List<ProductoListVm>();
        //    foreach (var product in productList)
        //    {
        //        var productVm = new ProductoListVm()
        //        {
        //            Id = product.Id,
        //            NombreCuchillo = product.Cuchillo.NombreCuchillo,
        //            Numero = product.Numero.Numeroo,
        //            Stock = product.Stock,
        //            Precio = product.Cuchillo.Precio
        //        };
        //        productListVm.Add(productVm);

        //    }
        //    return Json(new { data = productListVm });

        //}

        //    [HttpDelete]
        //    public IActionResult Delete(int? id)
        //    {
        //        if (id == null || id == 0)
        //        {
        //            return Json(new { success = false, message = "No encontrado" });

        //        }
        //        var productDelete = _unitOfWork.Productos.Get(p => p.Id == id);
        //        if (productDelete == null)
        //        {
        //            return Json(new { success = false, message = "Producto no encontrado" });
        //        }
        //        try
        //        {
        //            _unitOfWork.Productos.Delete(productDelete);
        //            _unitOfWork.Save();
        //            //var wwwRootPath = _webHostEnvironment.ContentRootPath;
        //            //if (productDelete.ImageUrl != null)
        //            //{
        //            //    var imageToDelete = Path
        //            //        .Combine(wwwRootPath, productDelete.ImageUrl.TrimStart('\\'));
        //            //    if (System.IO.File.Exists(imageToDelete))
        //            //    {
        //            //        System.IO.File.Delete(imageToDelete);
        //            //    }
        //            //}
        //            return Json(new { success = true, message = "¡Registro eliminado exitosamente!" });
        //        }
        //        catch (Exception ex)
        //        {

        //            return Json(new { success = false, message = ex.Message });
        //        }
        //    }
        //    #endregion





    }
}