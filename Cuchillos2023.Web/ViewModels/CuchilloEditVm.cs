using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.ViewModels
{
    public class CuchilloEditVm
    {
        public Cuchillo Cuchillo { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoriasList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MaterialesList { get; set; }
    }
}
