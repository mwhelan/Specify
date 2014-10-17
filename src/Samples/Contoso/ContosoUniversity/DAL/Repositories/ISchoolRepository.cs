using ContosoUniversity.Models;
using PagedList;

namespace ContosoUniversity.DAL.Repositories
{
    public interface ISchoolRepository
    {
        IPagedList<Student> GetStudents(string sortOrder, string searchString, int pageNumber, int pageSize);
        Student FindStudentById(int id);
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
    }
}