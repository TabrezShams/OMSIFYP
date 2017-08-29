using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMSIFYP.Models
{
    public class Student : Person
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public string oldschoolname { get; set; }

        public int oldclass { get; set; }

        public string schoolcertificate { get; set; }

        public string oldschaddress { get; set; }

        public string oldschcity { get; set; }

        [Required]
        public string fathercnic { get; set; }

        [StringLength(5,ErrorMessage ="Student Id Already Exist")]
        public string studId { get; set; }


      


        //1 to many relation
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}