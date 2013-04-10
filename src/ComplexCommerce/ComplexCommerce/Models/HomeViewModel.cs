using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ComplexCommerce.Models
{
    public class HomeViewModel
    {
        [Required()]
        [StringLength(40)]
        [Display]
        public string Name { get; set; }

        [Required, System.ComponentModel.DataAnnotations.Compare("Name", ErrorMessage = "Ange messy")]
        public int Age { get; set; }

        [Required, System.ComponentModel.DataAnnotations.Compare("Password2")]
        public string Password1 { get; set; }


        public string Password2 { get; set; }
    }
}