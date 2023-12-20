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
    
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int NumeroId { get; set; }
        [ValidateNever]

        public Numero Numero { get; set; }

        [Required]
        [Display(Name = "Cuchillo")]
        public int CuchilloId { get; set; }
        [ValidateNever]

        public Cuchillo Cuchillo { get; set; }


        [Required]
        [Range(1, 1000)]
        [Display(Name = "Stock")]
        public int Stock { get; set; }
    }
}
