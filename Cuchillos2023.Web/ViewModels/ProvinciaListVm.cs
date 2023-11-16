using System.ComponentModel.DataAnnotations;

namespace Cuchillos2023.Web.ViewModels
{
    public class ProvinciaListVm
    {
        public int Id { get; set; }
        [Display(Name = "Provincia")]
        public string NombreProvincia { get; set; }
        [Display(Name = "País")]
        public string NombrePais { get; set; }
    }
}
