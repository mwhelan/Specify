using System;
using System.Configuration;
using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        public SchoolContext():base(GetConnectionString())
        {
        }

        private static string GetConnectionString()
        {
            var environmentVariable = Environment.GetEnvironmentVariable("FunctionalTests");
            var connectionString = environmentVariable == null 
                ? ConfigurationManager.ConnectionStrings["SchoolContext"].ConnectionString 
                : ConfigurationManager.ConnectionStrings["SchoolContext-FunctionalTests"].ConnectionString;
            return connectionString;
        }


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
    }
}