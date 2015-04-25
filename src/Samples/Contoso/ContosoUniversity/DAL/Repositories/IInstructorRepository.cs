using System.Collections.Generic;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL.Repositories
{
    public interface IInstructorRepository
    {
        List<Instructor> Get();
        Instructor FindById(int id);
        void Create(Instructor course);
        void Update(Instructor course);
        void Delete(int id);
    }
}