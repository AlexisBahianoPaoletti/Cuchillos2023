using System.ComponentModel.DataAnnotations;

namespace Cuchillos2023.Web.ViewModels
{
    public class CiudadListVm
    {
        public int Id { get; set; }
        [Display(Name = "Ciudad")]
        public string NombreCiudad { get; set; }
        [Display(Name = "Provincia")]
        public string NombreProvincia { get; set; }
    }
}
