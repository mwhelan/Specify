using System.Web.Mvc;
using ContosoUniversity.AcceptanceTests.Infrastructure;
using ContosoUniversity.Controllers;
using ContosoUniversity.DAL.Repositories;
using ContosoUniversity.Models;
using Specify;

namespace ContosoUniversity.AcceptanceTests
{
    public class StudentDetailsStory : UserStory
    {
        public StudentDetailsStory()
        {
            AsA = "Student Administrator";
            IWant = "View the details of a Student";
            SoThat = "I can see what grade she achieved";
        }
    }

    public class ViewStudentDetailsScenario :
        ScenarioFor<ViewControllerScenario<StudentController, Student>, StudentDetailsStory>
    {
        ViewResult _result;
        private Student _student = new Student { ID = 1 };

        public void Given_an_existing_student()
        {
            DependencyFor<ISchoolRepository>()
                .FindStudentById(_student.ID)
                .Returns(_student);
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _result = SUT.ExecuteAction(c => c.Details(_student.ID));
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
