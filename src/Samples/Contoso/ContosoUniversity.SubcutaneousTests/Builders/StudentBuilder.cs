using System;
using ContosoUniversity.Models;
using TestStack.Dossier;

namespace ContosoUniversity.SubcutaneousTests.Builders
{
    public class StudentBuilder : TestDataBuilder<Student, StudentBuilder>
    {
        public virtual StudentBuilder WithId(int id)
        {
            return Set(x => x.Id, id);
        }

        public virtual StudentBuilder WithFirstName(string firstName)
        {
            return Set(x => x.FirstMidName, firstName);
        }

        public virtual StudentBuilder WithLastName(string lastName)
        {
            return Set(x => x.LastName, lastName);
        }

        public virtual StudentBuilder WithEnrollmentDate(DateTime enrollmentDate)
        {
            return Set(x => x.EnrollmentDate, enrollmentDate);
        }

        protected override Student BuildObject()
        {
            return new Student
            {
                Id = Get(x => x.Id),
                FirstMidName = Get(x => x.FirstMidName),
                LastName = Get(x => x.LastName),
                EnrollmentDate = Get(x => x.EnrollmentDate)
            };
        }
    }
}
