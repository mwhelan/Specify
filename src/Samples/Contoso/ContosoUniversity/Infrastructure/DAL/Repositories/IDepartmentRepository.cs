using System.Collections.Generic;
using ContosoUniversity.Domain.Model;

namespace ContosoUniversity.Infrastructure.DAL.Repositories
{
    public interface IDepartmentRepository
    {
        List<Department> Get();
        Department FindById(int id);
        void Create(Department course);
        void Update(Department course);
        void Delete(int id);
    }
}