using Cuchillos2023.DataLayer.Repository;
using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Utilities;
using Cuchillos2023.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cuchillos2023.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;

		[BindProperty]
		public OrderListVm OrderListVm { get; set; }
		public	List<OrderListVm> listOrderListVm = new List<OrderListVm>();
		public IEnumerable<OrderHeader> listOrderHeader = new List<OrderHeader>();
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
       
        public IActionResult Index()
		{
			listOrderHeader = _unitOfWork.OrderHeaders.GetAll(c=>c.Id>0);
			foreach (var order in listOrderHeader)
			{
                OrderListVm = new OrderListVm
                {
                    OrderHeader = _unitOfWork.OrderHeaders.Get(o => o.Id == order.Id, propertiesNames: "ApplicationUser"),
                    OrderDetail = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == order.Id, propertiesNames: "Producto")
                };
                listOrderListVm.Add(OrderListVm);
            }
            return View(listOrderListVm);
		}

		public IActionResult Details(int orderId)
		{
			OrderListVm = new OrderListVm
			{
				OrderHeader = _unitOfWork.OrderHeaders.Get(o => o.Id == orderId, propertiesNames: "ApplicationUser"),
				OrderDetail = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == orderId, propertiesNames: "Producto")
			};
			foreach (var orderListVm in OrderListVm.OrderDetail)
			{
				orderListVm.Producto=_unitOfWork.Productos.Get(p=>p.Id == orderListVm.ProductId);
				orderListVm.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == orderListVm.Producto.CuchilloId);
				orderListVm.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == orderListVm.Producto.NumeroId);
			}

			return View(OrderListVm);
		}

		[HttpPost]
		[Authorize(Roles = WC.Role_Admin)]
		[ValidateAntiForgeryToken]
		public IActionResult UpdateOrderDetail()
		{
			var orderHeaderFromDb = _unitOfWork.OrderHeaders
				.Get(o => o.Id == OrderListVm.OrderHeader.Id, tracked: false);
			//orderHeaderFromDb.Name = OrderListVm.OrderHeader.Name;
			//orderHeaderFromDb.StreetAddress = OrderListVm.OrderHeader.StreetAddress;
			//orderHeaderFromDb.State = OrderListVm.OrderHeader.State;
			//orderHeaderFromDb.City = OrderListVm.OrderHeader.City;
			//orderHeaderFromDb.ZipCode = OrderListVm.OrderHeader.ZipCode;
			//orderHeaderFromDb.PhoneNumber = OrderListVm.OrderHeader.PhoneNumber;
			//if (OrderListVm.OrderHeader.Carrier != null)
			//{
			//	orderHeaderFromDb.Carrier = OrderListVm.OrderHeader.Carrier;

			//}
			//if (OrderListVm.OrderHeader.TrackingNumber != null)
			//{
			//	orderHeaderFromDb.TrackingNumber = OrderListVm.OrderHeader.TrackingNumber;

			//}
			//_unitOfWork.OrderHeaders.Update(orderHeaderFromDb);
			//_unitOfWork.Save();
			//TempData["success"] = "order updated!!!";
			return RedirectToAction("Details", "Order", new { orderId = orderHeaderFromDb.Id });
		}


		#region API CALL
		[HttpGet]
		public IActionResult GetAll(string status)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			IEnumerable<OrderHeader> list;
			if (User.IsInRole(WC.Role_Admin))
			{
				list = _unitOfWork.OrderHeaders.GetAll(propertiesNames: "ApplicationUser");

			}
			else
			{
				list = _unitOfWork.OrderHeaders.GetAll(o => o.ApplicationUserId == userId.Value, propertiesNames: "ApplicationUser");

			}
			switch (status)
			{
				case "inprocess":
					list = list.Where(o => o.OrderStatus == WC.StatusInProgress).ToList();
					break;
				case "pending":
					list = list.Where(o => o.PaymentStatus == WC.PaymentStatusDelayedPayment).ToList();
					break;
				case "completed":
					list = list.Where(o => o.OrderStatus == WC.StatusShipped).ToList();
					break;
				case "approved":
					list = list.Where(o => o.OrderStatus == WC.StatusApproved).ToList();
					break;

				default:

					break;
			}


			return Json(new { data = list });

		}
		#endregion

	}
}
