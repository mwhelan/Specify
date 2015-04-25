using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public Course FindById(int id)
        {
            return Database.Courses.Single(x => x.Id == id);
        }

        public List<Course> Get()
        {
            return Database.Courses;
        }

        public void Create(Course student)
        {
            Database.Courses.Add(student);
        }

        public void Update(Course student)
        {
            var existingCourse = Database.Courses.Find(x => x.Id == student.Id);
            Database.Courses.Remove(existingCourse);
            Database.Courses.Add(student);
        }

        public void Delete(int id)
        {
            var existingCourse = Database.Courses.Find(x => x.Id == id);
            Database.Courses.Remove(existingCourse);
        }
    }
}