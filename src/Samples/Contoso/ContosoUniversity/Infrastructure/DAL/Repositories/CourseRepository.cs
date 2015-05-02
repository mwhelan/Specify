using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Domain.Model;

namespace ContosoUniversity.Infrastructure.DAL.Repositories
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

        public void Create(Course course)
        {
            course.Id = Database.Courses.Max(x => x.Id) + 1;
            Database.Courses.Add(course);
        }

        public void Update(Course course)
        {
            var existingCourse = Database.Courses.Find(x => x.Id == course.Id);
            Database.Courses.Remove(existingCourse);
            Database.Courses.Add(course);
        }

        public void Delete(int id)
        {
            var existingCourse = Database.Courses.Find(x => x.Id == id);
            Database.Courses.Remove(existingCourse);
        }
    }
}