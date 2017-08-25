using OMSIFYP.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OMSIFYP.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<SuperAdminCre> superadmin { get; set; }
        public DbSet<Fine> fine { get; set; }
        public DbSet<GenrateClass> genrateClass { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<SuperAdminContact> superAdminContact { get; set; }
        public DbSet<PTMCalls> ptmCall { get; set; }
        public DbSet<EnrollStudent> enrollStudent { get; set; }
        public DbSet<MessageSend> Message { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));

            modelBuilder.Entity<Department>().MapToStoredProcedures();
        }

        public System.Data.Entity.DbSet<OMSIFYP.Models.AdminContact> AdminContacts { get; set; }
    }
}