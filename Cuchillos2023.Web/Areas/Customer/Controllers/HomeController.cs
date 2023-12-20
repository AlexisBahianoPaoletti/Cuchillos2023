using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cuchillos2023.Models.Models;
using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.DataLayer.Repository;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Productos2023.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var productoList = _unitOfWork.Productos.GetAll(p=>p.Stock>0);
            List<ProductoListVm> productoListVm = new List<ProductoListVm>();
            var cuchilloList = _unitOfWork.Cuchillos.GetAll();
            var numeroList = _unitOfWork.Numeros.GetAll(); 
            foreach (var producto in productoList)
            {
                var productoVm = new ProductoListVm()
                {
                    Id = producto.Id,
                    Stock = producto.Stock

                };
                foreach (var cuchillo in cuchilloList)
                {
                    if (producto.CuchilloId == cuchillo.Id)
                    {
                        productoVm.NombreCuchillo = cuchillo.NombreCuchillo;
                        productoVm.Precio = cuchillo.Precio;
                        productoVm.ImagenUrl = cuchillo.ImagenUrl;
                    }
                }
                foreach (var numero in numeroList)
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

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var producto = _unitOfWork.Productos.Get(c => c.Id == id, "Cuchillo,Numero");
            producto.Cuchillo.Material = _unitOfWork.Materiales.Get(c => c.Id == producto.Cuchillo.MaterialId);
            producto.Cuchillo.Categoria = _unitOfWork.Categorias.Get(c => c.Id == producto.Cuchillo.CategoriaId);

            ShoppingCart cart = new ShoppingCart
            {
                ProductId = producto.Id,
                Producto = producto,
                Quantity = 1
            };
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            cart.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = userId.Value;
            var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.ApplicationUserId == userId.Value
                        && c.ProductId == cart.ProductId);
            if (cartInDb == null)
            {
                _unitOfWork.ShoppingCarts.Add(cart);

            }
            else
            {
                _unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, 1 /*cart.Quantity*/);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}