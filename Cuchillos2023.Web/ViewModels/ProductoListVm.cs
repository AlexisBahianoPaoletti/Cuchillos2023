using System.ComponentModel.DataAnnotations;


namespace Cuchillos2023.Web.ViewModels
{
    public class ProductoListVm
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public string NombreCuchillo { get; set; }
        public int Numero { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public string ImagenUrl { get; set; }
    }
}
