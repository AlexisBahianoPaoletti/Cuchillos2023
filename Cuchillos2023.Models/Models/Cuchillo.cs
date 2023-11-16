using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.Models.Models
{
    [Index(nameof(NombreCuchillo), IsUnique = true)]
    public class Cuchillo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Cuchillo")]
        [StringLength(200, ErrorMessage = "Debe estar entre {2} y {1}", MinimumLength = 3)]
        public string NombreCuchillo { get; set; }

        [Required]
        [Display(Name = "Material")]
        public int MaterialId { get; set; }

        [Required]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        [Display(Name = "Imagen")]
        [ValidateNever]
        public string ImagenUrl { get; set; }

        [Required]
        [Range(1, 30000)]
        [Display(Name = "Precio")]
        public double Precio { get; set; }

        [ValidateNever]
        public Material Material { get; set; }
        [ValidateNever]
        public Categoria Categoria { get; set; }

    }
}
