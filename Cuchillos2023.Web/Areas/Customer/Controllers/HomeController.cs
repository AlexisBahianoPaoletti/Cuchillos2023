using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cuchillos2023.Web.Areas.Customer.Controllers
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
            var cuchilloList = _unitOfWork.Cuchillos.GetAll();
            return View(cuchilloList);
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == id, "Categoria,Material");
            ShoppingCart cart = new ShoppingCart
            {
                Cuchillo = cuchillo,
                Quantity = 1
            };
            return View(cart);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}