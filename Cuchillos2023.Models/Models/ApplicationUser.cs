using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cuchillos2023.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
		public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }


		//[Required]
		//public int CiudadId { get; set; }
		//[ForeignKey("CiudadId")]
		//[ValidateNever]
		//public Ciudad Ciudad { get; set; }


        //public int CompanyId { get; set; }
        //[ForeignKey("CompanyId")]
        //[ValidateNever]
        //public Company Company { get; set; }
    } 
}
