using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Cuchillos2023.Models.Models
{
    [Index(nameof(NombreCategoria), IsUnique = true)]
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The field {0} must be between {2} y {1} characters", MinimumLength = 3)]
        [DisplayName("Categoría")]
        public string NombreCategoria { get; set; }

        //[Required]
        //[Range(1, 100, ErrorMessage = "The field {0} must be between {1} y {2}")]
        //public int DisplayOrder { get; set; }

    }
}
