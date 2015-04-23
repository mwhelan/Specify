using ContosoUniversity.Models;

namespace ContosoUniversity.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Database 
    {
        public static List<Course> Courses { get; set; }
        public static List<Department> Departments { get; set; }
        public static List<Enrollment> Enrollments { get; set; }
        public static List<Instructor> Instructors { get; set; }
        public static List<Student> Students { get; set; }
        public static List<OfficeAssignment> OfficeAssignments { get; set; }

        static Database()
        {
            Create();
        }

        public static void Create()
        {
            Students = new List<Student>
            {
                new Student { FirstMidName = "Carson",   LastName = "Alexander", 
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { FirstMidName = "Meredith", LastName = "Alonso",    
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Arturo",   LastName = "Anand",     
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Gytis",    LastName = "Barzdukas", 
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Yan",      LastName = "Li",        
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { FirstMidName = "Peggy",    LastName = "Justice",   
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { FirstMidName = "Laura",    LastName = "Norman",    
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { FirstMidName = "Nino",     LastName = "Olivetto",  
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            Instructors = new List<Instructor>
            {
                new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie", 
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",    
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstMidName = "Roger",   LastName = "Harui",       
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstMidName = "Candace", LastName = "Kapoor",      
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstMidName = "Roger",   LastName = "Zheng",      
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            Departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = Instructors.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = Instructors.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = Instructors.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    InstructorID  = Instructors.Single( i => i.LastName == "Kapoor").ID }
            };

            Courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = Departments.Single( s => s.Name == "Engineering").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmentID = Departments.Single( s => s.Name == "Economics").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmentID = Departments.Single( s => s.Name == "Economics").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = Departments.Single( s => s.Name == "Mathematics").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                    DepartmentID = Departments.Single( s => s.Name == "Mathematics").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                    DepartmentID = Departments.Single( s => s.Name == "English").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = Departments.Single( s => s.Name == "English").DepartmentID,
                    Instructors = new List<Instructor>() 
                },
            };

            OfficeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { 
                    InstructorID = Instructors.Single( i => i.LastName == "Fakhouri").ID, 
                    Location = "Smith 17" },
                new OfficeAssignment { 
                    InstructorID = Instructors.Single( i => i.LastName == "Harui").ID, 
                    Location = "Gowan 27" },
                new OfficeAssignment { 
                    InstructorID = Instructors.Single( i => i.LastName == "Kapoor").ID, 
                    Location = "Thompson 304" },
            };

            AddOrUpdateInstructor("Chemistry", "Kapoor");
            AddOrUpdateInstructor("Chemistry", "Harui");
            AddOrUpdateInstructor("Microeconomics", "Zheng");
            AddOrUpdateInstructor("Macroeconomics", "Zheng");

            AddOrUpdateInstructor("Calculus", "Fakhouri");
            AddOrUpdateInstructor("Trigonometry", "Harui");
            AddOrUpdateInstructor("Composition", "Abercrombie");
            AddOrUpdateInstructor("Literature", "Abercrombie");

            Enrollments = new List<Enrollment>
            {
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Alexander").ID, 
                    CourseID = Courses.Single(c => c.Title == "Chemistry" ).CourseID, 
                    Grade = Grade.A 
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = Courses.Single(c => c.Title == "Microeconomics" ).CourseID, 
                    Grade = Grade.C 
                },                            
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = Courses.Single(c => c.Title == "Macroeconomics" ).CourseID, 
                    Grade = Grade.B
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = Courses.Single(c => c.Title == "Calculus" ).CourseID, 
                    Grade = Grade.B 
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = Courses.Single(c => c.Title == "Trigonometry" ).CourseID, 
                    Grade = Grade.B 
                },
                new Enrollment {
                    StudentID = Students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = Courses.Single(c => c.Title == "Composition" ).CourseID, 
                    Grade = Grade.B 
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Anand").ID,
                    CourseID = Courses.Single(c => c.Title == "Chemistry" ).CourseID
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Anand").ID,
                    CourseID = Courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B         
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = Courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B         
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Li").ID,
                    CourseID = Courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B         
                },
                new Enrollment { 
                    StudentID = Students.Single(s => s.LastName == "Justice").ID,
                    CourseID = Courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B         
                }
            };

            foreach (Enrollment e in Enrollments)
            {
                var enrollmentInDataBase = Enrollments.Where(
                    s =>
                        s.Student.ID == e.StudentID &&
                        s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    Enrollments.Add(e);
                }
            }
        }

        static void AddOrUpdateInstructor(string courseTitle, string instructorName)
        {
            var crs = Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
                crs.Instructors.Add(Instructors.Single(i => i.LastName == instructorName));
        }

    }
}