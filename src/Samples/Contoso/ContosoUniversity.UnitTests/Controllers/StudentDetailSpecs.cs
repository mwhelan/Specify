using System.Net;
using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.DAL.Repositories;
using ContosoUniversity.Models;
using FluentAssertions;
using NSubstitute;
using Specify.WithMsTest;

namespace ContosoUniversity.UnitTests.Controllers
{
    public class DetailsForExistingStudent : SpecificationFor<StudentController>
    {
        ViewResult _result;
        private Student _student = new Student {ID = 1};

        public void Given_an_existing_student()
        {
            The<ISchoolRepository>()
                .FindStudentById(_student.ID)
                .Returns(_student);
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _result = Subject.Details(_student.ID) as ViewResult;
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

    public class DetailsForNonExistentStudent : SpecificationFor<StudentController>
    {
        ActionResult _result;

        public void Given_a_student_does_not_exist()
        {
            The<ISchoolRepository>()
                .FindStudentById(Arg.Any<int>())
                .Returns((Student)null);
        }

        public void When_the_student_details_are_requested()
        {
            _result = Subject.Details(10);
        }

        public void Then_HttpNotFound_is_returned()
        {
            _result.Should().BeOfType<HttpNotFoundResult>();
        }
    }

    public class DetailsForNoId : SpecificationFor<StudentController>
    {
        ActionResult _result;

        public void When_a_null_Id_is_requested()
        {
            _result = Subject.Details(null);
        }

        public void Then_a_bad_request_is_returned()
        {
            _result.Should().BeOfType<HttpStatusCodeResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
