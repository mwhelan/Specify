using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.Domain.Model;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ContosoUniversity.UnitTests.Controllers
{
    public class DetailsForExistingStudent : ScenarioFor<StudentController>
    {
        ViewResult _result;
        private Student _student = new Student {Id = 1};

        public void Given_an_existing_student()
        {
            Container.Get<IStudentRepository>()
                .FindById(_student.Id)
                .Returns(_student);
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _result = SUT.Details(_student.Id) as ViewResult;
        }

        public void Then_the_details_view_is_displayed()
        {
            _result.ViewName.Should().Be(string.Empty);
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            _result.Model.Should().Be(_student);
        }
    }
}
