using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using ContosoUniversity.Models;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using FluentAssertions;
using TestStack.FluentMVCTesting;

namespace ContosoUniversity.SubcutaneousTests.Specifications.Students
{
    public class ViewStudentDetailsScenario : ScenarioFor<MvcControllerDriver, StudentDetailsStory>
    {
        private Student _student;
        private ControllerResultTest<StudentController> _result; 

        public void Given_an_existing_student()
        {
            _student = Container.Get<IStudentRepository>().FindById(1);
        }
        public void When_the_details_are_requested_for_that_student()
        {
            _result = SUT.ExecuteActionFor<StudentController>(c => c.Details(_student.Id));
        }

        public void Then_the_details_view_is_displayed()
        {
            (_result.ActionResult as ViewResult).ViewBag.Title = "Details";
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            _result.ShouldRenderDefaultView().WithModel(_student);
        }

        public void AndThen_the_enrollments_for_the_student_are_displayed()
        {
            var model = (_result.ActionResult as ViewResult).Model as Student;
            model.Enrollments.Count.Should().Be(3);
        }
    }
}