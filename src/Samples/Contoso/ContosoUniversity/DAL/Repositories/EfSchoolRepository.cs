using System;
using System.Data.Entity;
using System.Linq;
using ContosoUniversity.Models;
using PagedList;

namespace ContosoUniversity.DAL.Repositories
{
    public class EfSchoolRepository : ISchoolRepository
    {
        private SchoolContext _db;

        public EfSchoolRepository(SchoolContext db)
        {
            _db = db;
        }

        public IPagedList<Student> GetStudents(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var students = from s in _db.Students
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
            return _db.Students.Find(id);
        }

        public void Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }

        public void Update(Student student)
        {
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Student student = _db.Students.Find(id);
            _db.Students.Remove(student);
            _db.SaveChanges();
        }
    }
}