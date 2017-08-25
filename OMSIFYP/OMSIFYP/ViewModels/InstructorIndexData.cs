using System.Collections.Generic;
using OMSIFYP.Models;

namespace OMSIFYP.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
    public class AdminData
    {
        public IEnumerable<Admin> Admins { get; set; }
    }
    public class DepartIndex
    {
        public IEnumerable<Department> depart { get; set; }

    }
}

