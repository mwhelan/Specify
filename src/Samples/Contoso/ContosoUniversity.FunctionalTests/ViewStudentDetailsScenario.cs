using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using FluentAssertions;
using Specify.xUnit;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Actions;

namespace ContosoUniversity.FunctionalTests
{
    public class ViewStudentDetailsScenario : ScenarioFor<BrowserHost, StudentDetailsStory>
    {
        private Student _student = new Student { ID = 1 };
        private ViewDetailsPage _page;

        public void Given_an_existing_student()
        {
           
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _page = SUT.Host.NavigateToInitialPage<StudentController, ViewDetailsPage>(c => c.Details(_student.ID));
        }

        public void Then_the_details_view_is_displayed()
        {
            _page.Title.Should().Be("Details - Contoso University");
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            Student student = _page.ReadModelFromPage();
            student.ID.Should().Be(1);
            student.FirstMidName.Should().Be("Carson");
            student.LastName.Should().Be("Alexander");
        }

        public void AndThen_the_enrollments_for_the_student_are_displayed()
        {
            //var enrollments = _page.Enrollments();
            //enrollments.NumberOfRows.Should().Be(3);
        }
    }

    public class ViewDetailsPage : Page<Student>
    {
        public Student ReadModelFromPage()
        {
            return Read.ModelFromPage();
        }

        public TableReader<Enrollment> Enrollments()
        {
            return TableFor<Enrollment>("Enrollments");
        }
    }
}