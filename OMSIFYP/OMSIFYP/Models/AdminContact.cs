using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class AdminContact
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Message Title")]
        public string Titile { get; set; }
        [Required]
        public string Message { get; set; }
    }
}