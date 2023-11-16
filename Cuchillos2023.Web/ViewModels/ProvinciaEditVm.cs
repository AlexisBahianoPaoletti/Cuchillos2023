using Cuchillos2023.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cuchillos2023.Web.ViewModels
{
    public class ProvinciaEditVm
    {
        public Provincia Provincia { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PaisesList { get; set; }
    }
}
