using System;
using ContosoUniversity.Models;
using PagedList;

namespace ContosoUniversity.DAL.Repositories
{
    using System.Linq;

    public class EfSchoolRepository : ISchoolRepository
    {
        public IPagedList<Student> GetStudents(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var students = from s in Database.Students
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students
                    .Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
                                || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            return students.ToPagedList(pageNumber, pageSize);
        }

        public Student FindStudentById(int id)
        {
            return Database.Students.Single(x => x.ID == id);
        }

        public void Create(Student student)
        {
            Database.Students.Add(student);
        }

        //public void Update(Student student)
        //{
        //    var student = Database.Students.Single(x => x.ID == student.ID);
        //    = student;
        //    Database.Entry(student).State = EntityState.Modified;
        //    Database.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    Student student = Database.Students.Find(id);
        //    Database.Students.Remove(student);
        //    Database.SaveChanges();
        //}
    }
}