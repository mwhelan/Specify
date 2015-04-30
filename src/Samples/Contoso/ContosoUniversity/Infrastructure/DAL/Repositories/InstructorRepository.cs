using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Models;

namespace ContosoUniversity.Infrastructure.DAL.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        public List<Instructor> Get()
        {
            return Database.Instructors;
        }

        public Instructor FindById(int id)
        {
            return Database.Instructors.Single(x => x.Id == id);
        }

        public void Create(Instructor student)
        {
            Database.Instructors.Add(student);
        }

        public void Update(Instructor student)
        {
            var existingInstructor = Database.Instructors.Find(x => x.Id == student.Id);
            Database.Instructors.Remove(existingInstructor);
            Database.Instructors.Add(student);
        }

        public void Delete(int id)
        {
            var existingInstructor = Database.Instructors.Find(x => x.Id == id);
            Database.Instructors.Remove(existingInstructor);
        }
    }
}