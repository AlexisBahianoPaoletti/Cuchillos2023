﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuchillos2023.Models.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[Display(Name = "Producto")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Producto Producto { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }



        //[Required]
        //[Range(1, 1000)]
        [Display(Name = "Cantidad")]
        [Range(1, 1000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Quantity { get; set; }

        
        public double Precio { get; set; }
    }
}
