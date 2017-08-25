using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMSIFYP.Models
{
    public class EnrollStudent
    {
        public int ID { get; set; }
        public int GenrateClassID { get; set; }
        public int StudentID { get; set; }
        public int sessional1 { get; set; }
        public int sessional2 { get; set; }
        public int sessional3 { get; set; }
        public int total { get {
                return sessional1 + sessional2 + sessional3;
            } }



        virtual public GenrateClass genrateClass { get; set; }
        virtual public Student student { get; set; }
    }
}