using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.Models.Models
{
    [Index(nameof(NombreMaterial), IsUnique = true)]
    public class Material
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Material")]
        public string NombreMaterial { get; set; }

    }
}
