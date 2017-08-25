using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class SuperAdminCre
    {
        public int SuperAdminCreID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string pass { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string imgUrl { get; set; }



    }
}