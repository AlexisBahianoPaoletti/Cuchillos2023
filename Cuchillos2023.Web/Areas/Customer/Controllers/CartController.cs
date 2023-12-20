using Cuchillos2023.DataLayer.Repository.Interfaces;
using Cuchillos2023.Models.Models;
using Cuchillos2023.Utilities;
using Cuchillos2023.Web.ViewModels;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;


namespace Cuchillos2023.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public ShoppingCartVm shoppingCartVm { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Producto"),
				OrderHeader = new()

			};

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == itemCart.Producto.CuchilloId);
				itemCart.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == itemCart.Producto.NumeroId);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Producto.Cuchillo.Precio;
			}

			return View(shoppingCartVm);
		}

		[HttpGet]
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Producto"),
				OrderHeader = new()
			};
			shoppingCartVm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUsers.Get(u => u.Id == userId.Value);


			shoppingCartVm.OrderHeader.Name = shoppingCartVm.OrderHeader.ApplicationUser.Name;
			shoppingCartVm.OrderHeader.StreetAddress = shoppingCartVm.OrderHeader.ApplicationUser.StreetAddress;
			shoppingCartVm.OrderHeader.City = shoppingCartVm.OrderHeader.ApplicationUser.City;
			shoppingCartVm.OrderHeader.State = shoppingCartVm.OrderHeader.ApplicationUser.State;
			shoppingCartVm.OrderHeader.PhoneNumber = shoppingCartVm.OrderHeader.ApplicationUser.PhoneNumber;
			shoppingCartVm.OrderHeader.ZipCode = shoppingCartVm.OrderHeader.ApplicationUser.ZipCode;

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == itemCart.Producto.CuchilloId);
				itemCart.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == itemCart.Producto.NumeroId);
				//shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Producto.Cuchillo.Precio;
			}
			MercadoPagoConfig.AccessToken = "TEST-1873877444627449-121619-72ba60b4616ba168aaca763c0ee86b65-1037972918";
			//return View(shoppingCart);
			var preference = CrearPreferencia();
			shoppingCartVm.Preference= preference;
			return View(shoppingCartVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			shoppingCartVm.CartList = _unitOfWork.ShoppingCarts
				.GetAll(c => c.ApplicationUserId == userId.Value, propertiesNames: "Producto");

			shoppingCartVm.OrderHeader.OrderDate = DateTime.Now;
			shoppingCartVm.OrderHeader.ApplicationUserId = userId.Value;
			shoppingCartVm.OrderHeader.OrderStatus = WC.StatusApproved;
			


			//ApplicationUser appUser = _unitOfWork.ApplicationUsers.Get(u => u.Id == userId.Value);
			//if (appUser.CompanyId.GetValueOrDefault() == 0)
			//{
			//    shoppingCartVm.OrderHeader.PaymentStatus = WC.PaymentStatusPending;
			//    shoppingCartVm.OrderHeader.OrderStatus = WC.StatusPending;

			//}
			//else
			//{
			//    shoppingCartVm.OrderHeader.PaymentStatus = WC.PaymentStatusDelayedPayment;
			//    shoppingCartVm.OrderHeader.OrderStatus = WC.StatusApproved;

			//}

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == itemCart.Producto.CuchilloId);
				itemCart.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == itemCart.Producto.NumeroId);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Producto.Cuchillo.Precio;
				itemCart.Producto.Stock -= itemCart.Quantity;
			}

			try
			{
				//_unitOfWork.BeginTransaction();
				_unitOfWork.OrderHeaders.Add(shoppingCartVm.OrderHeader);
				_unitOfWork.Save();
				foreach (var itemCart in shoppingCartVm.CartList)
				{
					var orderDetail = new OrderDetail
					{
						OrderId = shoppingCartVm.OrderHeader.Id,
						ProductId = itemCart.ProductId,
						Quantity = itemCart.Quantity,
						Price = itemCart.Producto.Cuchillo.Precio
					};
					_unitOfWork.OrderDetails.Add(orderDetail);
				}
				_unitOfWork.Save();





				//else
				//{
				_unitOfWork.ShoppingCarts.RemoveRange(shoppingCartVm.CartList);
				_unitOfWork.Save();

				_unitOfWork.CommitTransaction();
				return RedirectToAction("OrderConfirmation", "Cart",
					new { id = shoppingCartVm.OrderHeader.Id });

				//}

			}
			catch (Exception)
			{
				_unitOfWork.RollbackTransaction();
				return View("Index");

			}

		}

		public async Task<Preference> CrearPreferencia()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == itemCart.Producto.CuchilloId);
				itemCart.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == itemCart.Producto.NumeroId);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Producto.Cuchillo.Precio;
			}
			// Crea el objeto de request de la preference
			var request = new PreferenceRequest
			{
				// the Purpose = 'wallet_purchase', allows only logged payments.
				// to allow guest payments you can omit this property
				Purpose = "wallet_purchase",
				Items = new List<PreferenceItemRequest>
		{
			new PreferenceItemRequest
			{
				Title = "My product",
				Quantity = 1,
				CurrencyId = "BRL",
				UnitPrice = 75.56m,
			},
		},
			};

			// Create the preference using the client
			var client = new PreferenceClient();
			Preference preference = await client.CreateAsync(request);
			return preference;
		}


		public IActionResult OrderConfirmation(int id)
		{

			var orderHeader = _unitOfWork.OrderHeaders.Get(o => o.Id == id);


			return View(id);

		}

		public IActionResult Plus(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId, "Producto");
			if (cartInDb.Quantity < cartInDb.Producto.Stock)
			{
				_unitOfWork.ShoppingCarts.IncrementQuantity(cartInDb, 1);
				_unitOfWork.Save();
			}
			return RedirectToAction("Index");
		}
		public IActionResult Minus(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			if (cartInDb.Quantity == 1)
			{
				_unitOfWork.ShoppingCarts.Delete(cartInDb);
			}
			else
			{
				_unitOfWork.ShoppingCarts.DecrementQuantity(cartInDb, 1);

			}
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		public IActionResult RemoveFromCart(int cartId)
		{
			var cartInDb = _unitOfWork.ShoppingCarts.Get(c => c.Id == cartId);
			_unitOfWork.ShoppingCarts.Delete(cartInDb);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}








		#region API CALLS
		[HttpDelete]
		public IActionResult Delete(int id)
		{

			try
			{
				var cart = _unitOfWork.ShoppingCarts.Get(c => c.Id == id);
				_unitOfWork.ShoppingCarts.Delete(cart);
				_unitOfWork.Save();
				return Json(new { success = true, message = "¡Carrito eliminado satisfactoriamente!" });

			}
			catch (Exception)
			{

				return Json(new { success = false, message = "Problemas al intentar retirar un carrito." });

			}
		}

		[HttpPost]
		public IActionResult GetTotal()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			var shoppingCartVm = new ShoppingCartVm
			{
				CartList = _unitOfWork.ShoppingCarts
				.GetAll(s => s.ApplicationUserId == claims.Value, propertiesNames: "Producto")
			};
			shoppingCartVm.OrderHeader = new OrderHeader() { OrderTotal = 0 };
			foreach (var itemCart in shoppingCartVm.CartList)
			{
				itemCart.Producto.Cuchillo = _unitOfWork.Cuchillos.Get(c => c.Id == itemCart.Producto.CuchilloId);
				itemCart.Producto.Numero = _unitOfWork.Numeros.Get(c => c.Id == itemCart.Producto.NumeroId);
				shoppingCartVm.OrderHeader.OrderTotal += itemCart.Quantity * itemCart.Producto.Cuchillo.Precio;
			}
			//double total = 0;
			//double? test = shoppingCartVm.OrderHeader.OrderTotal;

			//if (test!=null)
			//         {
			var total = shoppingCartVm.OrderHeader.OrderTotal;

			//}
			return Json(total);
		}
		#endregion

	}
}
