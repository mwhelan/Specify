using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;
using PagedList;

namespace ContosoUniversity.DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public IPagedList<Student> Get(string sortOrder, string searchString, int pageNumber, int pageSize)
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

        public List<Student> Get()
        {
            return Database.Students;
        } 

        public Student FindById(int id)
        {
            return Database.Students.Single(x => x.Id == id);
        }

        public void Create(Student student)
        {
            Database.Students.Add(student);
        }

        public void Update(Student student)
        {
            var existingStudent = Database.Students.Find(x => x.Id == student.Id);
            Database.Students.Remove(existingStudent);
            Database.Students.Add(student);
        }

        public void Delete(int id)
        {
            var existingStudent = Database.Students.Find(x => x.Id == id);
            Database.Students.Remove(existingStudent);
        }
    }
}