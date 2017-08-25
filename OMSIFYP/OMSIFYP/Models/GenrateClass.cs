using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public enum section
    {
        A, B, C, D, F
    }
    public class GenrateClass
    {
        public int GenrateClassID { get; set; }
        public string Name { get; set; }
        public section? Section { get; set; }
        public int CourseID { get; set; }
        public int InstructorID { get; set; }
        public int DepartmentID { get; set; }
        virtual public Course course { get; set; }
        virtual public Instructor instructor { get; set; }
        virtual public Department department { get; set; }
        virtual public ICollection<Student> students { get; set; }
    }
}