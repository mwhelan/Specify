using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.Domain.Model;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ContosoUniversity.UnitTests.Controllers
{
    public class DetailsForNonExistentStudent : ScenarioFor<StudentController>
    {
        ActionResult _result;

        public void Given_a_student_does_not_exist()
        {
            Container.Get<IStudentRepository>()
                .FindById(Arg.Any<int>())
                .Returns((Student)null);
        }

        public void When_the_student_details_are_requested()
        {
            _result = SUT.Details(10);
        }

        public void Then_HttpNotFound_is_returned()
        {
            _result.Should().BeOfType<HttpNotFoundResult>();
        }
    }
}