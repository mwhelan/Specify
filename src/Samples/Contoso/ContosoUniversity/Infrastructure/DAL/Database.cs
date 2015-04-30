using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.Infrastructure.DAL
{
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
            AddPrimaryKeys(Students);

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
            AddPrimaryKeys(Instructors);

            Departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    Instructor  = Instructors.Single( i => i.LastName == "Abercrombie") },
                new Department { Name = "Mathematics", Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    Instructor  = Instructors.Single( i => i.LastName == "Fakhouri") },
                new Department { Name = "Engineering", Budget = 350000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    Instructor  = Instructors.Single( i => i.LastName == "Harui") },
                new Department { Name = "Economics",   Budget = 100000, 
                    StartDate = DateTime.Parse("2007-09-01"), 
                    Instructor  = Instructors.Single( i => i.LastName == "Kapoor") }
            };
            AddPrimaryKeys(Departments);

            Courses = new List<Course>
            {
                new Course {Id = 1050, Title = "Chemistry",      Credits = 3,
                    Department = Departments.Single( s => s.Name == "Engineering")
                },
                new Course {Id = 4022, Title = "Microeconomics", Credits = 3,
                    Department = Departments.Single( s => s.Name == "Economics")
                },
                new Course {Id = 4041, Title = "Macroeconomics", Credits = 3,
                    Department = Departments.Single( s => s.Name == "Economics")
                },
                new Course {Id = 1045, Title = "Calculus",       Credits = 4,
                    Department = Departments.Single( s => s.Name == "Mathematics")
                },
                new Course {Id = 3141, Title = "Trigonometry",   Credits = 4,
                    Department = Departments.Single( s => s.Name == "Mathematics")
                },
                new Course {Id = 2021, Title = "Composition",    Credits = 3,
                    Department = Departments.Single( s => s.Name == "English")
                },
                new Course {Id = 2042, Title = "Literature",     Credits = 4,
                    Department = Departments.Single( s => s.Name == "English")
                },
            };
            AddPrimaryKeys(Courses);

            OfficeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment { 
                    Instructor = Instructors.Single( i => i.LastName == "Fakhouri"), 
                    Location = "Smith 17" },
                new OfficeAssignment { 
                    Instructor = Instructors.Single( i => i.LastName == "Harui"), 
                    Location = "Gowan 27" },
                new OfficeAssignment { 
                    Instructor = Instructors.Single( i => i.LastName == "Kapoor"), 
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
                    Student = Students.Single(s => s.LastName == "Alexander"), 
                    Course = Courses.Single(c => c.Title == "Chemistry" ), 
                    Grade = Grade.A 
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Alexander"),
                    Course = Courses.Single(c => c.Title == "Microeconomics" ), 
                    Grade = Grade.C 
                },                            
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Alexander"),
                    Course = Courses.Single(c => c.Title == "Macroeconomics" ), 
                    Grade = Grade.B
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Alonso"),
                    Course = Courses.Single(c => c.Title == "Calculus" ), 
                    Grade = Grade.B 
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Alonso"),
                    Course = Courses.Single(c => c.Title == "Trigonometry" ), 
                    Grade = Grade.B 
                },
                new Enrollment {
                    Student = Students.Single(s => s.LastName == "Alonso"),
                    Course = Courses.Single(c => c.Title == "Composition" ), 
                    Grade = Grade.B 
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Anand"),
                    Course = Courses.Single(c => c.Title == "Chemistry" )
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Anand"),
                    Course = Courses.Single(c => c.Title == "Microeconomics"),
                    Grade = Grade.B         
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Barzdukas"),
                    Course = Courses.Single(c => c.Title == "Chemistry"),
                    Grade = Grade.B         
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Li"),
                    Course = Courses.Single(c => c.Title == "Composition"),
                    Grade = Grade.B         
                },
                new Enrollment { 
                    Student = Students.Single(s => s.LastName == "Justice"),
                    Course = Courses.Single(c => c.Title == "Literature"),
                    Grade = Grade.B         
                }
            };
            AddPrimaryKeys(Enrollments);

            foreach (Enrollment enrollment in Enrollments)
            {
                Debug.WriteLine(enrollment);

                Students
                    .Single(x => x.Id == enrollment.StudentId)
                    .Enrollments
                    .Add(enrollment);
                
                Courses
                    .Single(x => x.Id == enrollment.CourseId)
                    .Enrollments
                    .Add(enrollment);
            }
        }

        static void AddOrUpdateInstructor(string courseTitle, string instructorName)
        {
            var course = Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = course.Instructors.SingleOrDefault(i => i.LastName == instructorName);
            if (inst == null)
            {
                var instructor = Instructors.Single(i => i.LastName == instructorName);
                course.Instructors.Add(instructor);
            }
        }

        private static void AddPrimaryKeys(IEnumerable<IEntity> entities)
        {
            int id = 0;
            foreach (var entity in entities)
            {
                id++;
                entity.Id = id;
            }
        }
    }
}