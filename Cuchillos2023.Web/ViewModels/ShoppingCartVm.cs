using Cuchillos2023.Models.Models;
using MercadoPago.Resource.Preference;

namespace Cuchillos2023.Web.ViewModels
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> CartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public Task<Preference> Preference { get; set; }
    }
}
