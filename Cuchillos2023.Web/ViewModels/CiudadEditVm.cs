using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.ViewModels
{
    public class CiudadEditVm
    {
        public Ciudad Ciudad { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ProvinciasList { get; set; }
    }
}
