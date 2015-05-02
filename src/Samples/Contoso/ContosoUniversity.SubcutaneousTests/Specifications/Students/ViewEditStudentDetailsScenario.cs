using System.Web.Mvc;
using ContosoUniversity.Controllers;
using ContosoUniversity.Domain.Model;
using ContosoUniversity.Infrastructure.DAL.Repositories;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using TestStack.FluentMVCTesting;

namespace ContosoUniversity.SubcutaneousTests.Specifications.Students
{
    public class ViewEditStudentDetailsScenario : ScenarioFor<MvcControllerDriver, StudentEditStory>
    {
        private Student _student;
        private ControllerResultTest<StudentController> _result;

        public void Given_an_existing_student()
        {
            _student = Container.Get<IStudentRepository>().FindById(1);
        }
        public void When_I_choose_to_edit_student_details()
        {
            _result = SUT.ExecuteActionFor<StudentController>(c => c.Edit(_student.Id));
        }

        public void Then_the_edit_view_is_displayed()
        {
            (_result.ActionResult as ViewResult).ViewBag.Title = "Edit";
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            _result.ShouldRenderDefaultView().WithModel(_student);
        }
    }
}
