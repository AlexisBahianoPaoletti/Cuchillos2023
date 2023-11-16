using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    [Index(nameof(NombreCiudad), IsUnique = true)]
    public class Ciudad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Ciudad")]
        public string NombreCiudad { get; set; }

        [Required]
        [Display(Name = "Provincia")]
        public int ProvinciaId { get; set; }

        [ValidateNever]
        public Provincia Provincia { get; set; }
    }
}
