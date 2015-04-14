using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using FluentAssertions;
using NUnit.Framework;
using Specify;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Actions;

namespace ContosoUniversity.FunctionalTests.Specifications
{
    public class ViewStudentDetailsScenario : ScenarioFor<BrowserHost, StudentDetailsStory>
    {
        private Student _student = new Student { ID = 1 };
        private StudentDetailsPage _page;

        public void Given_an_existing_student()
        {
           
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _page = SUT.Host.NavigateToInitialPage<StudentController, StudentDetailsPage>(c => c.Details(_student.ID));
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

        //public void AndThen_the_enrollments_for_the_student_are_displayed()
        //{
        //    var enrollments = _page.Enrollments();
        //    enrollments.NumberOfRows.Should().Be(3);
        //}

        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }

    public class StudentDetailsPage : Page<Student>
    {
        private Student _student;
        public Student ReadModelFromPage()
        {
            if (_student == null)
            {
                _student = Read.ModelFromPage();
            }
            return _student;
        }

        public TableReader<Enrollment> Enrollments()
        {
            return TableFor<Enrollment>("Enrollments");
        }
    }
}