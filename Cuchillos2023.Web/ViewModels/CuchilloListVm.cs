using System.ComponentModel.DataAnnotations;


namespace Cuchillos2023.Web.ViewModels
{
    public class CuchilloListVm
    {
        public int Id { get; set; }

        [Display(Name = "Cuchillo")]
        public string NombreCuchillo { get; set; }
        public double Precio { get; set; }
    }
}
