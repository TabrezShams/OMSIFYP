using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSIFYP.Models
{
    public class Instructor : employee
    {
        public string time { get; set; }

        public string orgaddress { get; set; }

        public string oldjob { get; set; }

        public string position { get; set; }

        public string instit { get; set; }



        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}