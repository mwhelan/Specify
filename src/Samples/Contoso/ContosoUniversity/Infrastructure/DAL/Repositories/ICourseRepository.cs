using System.Collections.Generic;
using ContosoUniversity.Models;

namespace ContosoUniversity.Infrastructure.DAL.Repositories
{ 
    public interface ICourseRepository
    {
        Course FindById(int id);
        List<Course> Get(); 
        void Create(Course course);
        void Update(Course course);
        void Delete(int id);
    }
}