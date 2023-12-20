using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cuchillos2023.Models.Models
{
    [Index(nameof(Numeroo), IsUnique = true)]
    public class Numero
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //[StringLength(20, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        [DisplayName("Número")]
        [Range(1, 30)]
        public int Numeroo { get; set; }

    }
}

