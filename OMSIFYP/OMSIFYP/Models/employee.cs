using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSIFYP.Models
{
    public class employee : Person
    {




        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        public int salary { get; set; }

        [Required]
        public string cv { get; set; }

        [Required]
        public string qualificatin { get; set; }

       
    }
}