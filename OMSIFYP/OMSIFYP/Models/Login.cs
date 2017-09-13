using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class Login
    {
        public string email { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
    }
}