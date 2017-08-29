using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class ContactUs
    {
        public int ID { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string message { get; set; }

        [Required]
        public string phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Email]
        public string email { get; set; }



    }
}