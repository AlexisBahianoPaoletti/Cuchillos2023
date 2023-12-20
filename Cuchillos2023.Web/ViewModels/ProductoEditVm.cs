using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.ViewModels
{
    public class ProductoEditVm
    {
        public Producto Producto { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CuchillosList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> NumerosList { get; set; }
    }
}
