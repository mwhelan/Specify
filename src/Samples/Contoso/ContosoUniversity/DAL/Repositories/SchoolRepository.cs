using System;
using System.Linq;
using ContosoUniversity.Models;
using MicroLite;
using PagedList;

namespace ContosoUniversity.DAL.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ISession _session;

        public SchoolRepository(ISession session)
        {
            _session = session;
        }

        public IPagedList<Student> GetStudents(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            PagedResult<Student> result;

            using (var transaction = _session.BeginTransaction())
            {
                var query = new SqlQuery(
                    "SELECT [ID] ,[LastName] ,[FirstName] , [EnrollmentDate] " +
                    "FROM [dbo].[Student] ");

                result = _session.Paged<Student>(query, PagingOptions.ForPage(page: pageNumber, resultsPerPage: pageSize));

                transaction.Commit();
            }
            return new PagedList<Student>(result.Results.AsEnumerable(), result.Page, result.ResultsPerPage);
        }

        public Student FindStudentById(int id)
        {
            Student student;
            using (var transaction = _session.BeginTransaction())
            {
                //var query = new SqlQuery(
                //    "SELECT [ID] ,[LastName] ,[FirstName] ,[HireDate] ,[EnrollmentDate] " +
                //    "FROM [dbo].[Person] " +
                //    "WHERE [ID] = @id", id);

                 student = _session.Single<Student>(id);

                transaction.Commit();
            }
            return student;
        }

        public void Create(Student student)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}