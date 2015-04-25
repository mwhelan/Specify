using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public List<Department> Get()
        {
            return Database.Departments;
        }

        public Department FindById(int id)
        {
            return Database.Departments.Single(x => x.Id == id);
        }

        public void Create(Department student)
        {
            Database.Departments.Add(student);
        }

        public void Update(Department student)
        {
            var existingDepartment = Database.Departments.Find(x => x.Id == student.Id);
            Database.Departments.Remove(existingDepartment);
            Database.Departments.Add(student);
        }

        public void Delete(int id)
        {
            var existingDepartment = Database.Departments.Find(x => x.Id == id);
            Database.Departments.Remove(existingDepartment);
        }
    }
}