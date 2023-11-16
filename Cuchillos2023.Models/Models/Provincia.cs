using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuchillos2023.Models.Models
{
    [Index(nameof(NombreProvincia), IsUnique = true)]
    public class Provincia
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Provincia")]
        public string NombreProvincia { get; set; }

        [Required]
        [Display(Name = "País")]
        public int PaisId { get; set; }

        [ValidateNever]
        public Pais Pais { get; set; }
    }
}
