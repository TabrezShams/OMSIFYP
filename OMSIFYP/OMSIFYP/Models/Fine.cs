using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class Fine
    {
        public int FineID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int FineAmount { get; set; }

        public int amountCollected { get; set; }



        virtual public Student student { get; set; }

    }
}