using System.Web.Mvc;
using ContosoUniversity.AcceptanceTests.Infrastructure;
using ContosoUniversity.Controllers;
using ContosoUniversity.Models;
using FluentAssertions;
using NUnit.Framework;
using Specify;

namespace ContosoUniversity.AcceptanceTests.Specifications
{
    public class ViewStudentDetailsScenario 
        : ScenarioFor<ViewControllerScenario<StudentController,Student>, StudentDetailsStory>
    {
        private Student _student = new Student { ID = 1 };

        public void Given_an_existing_student()
        {
           
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            SUT.ExecuteAction(c => (ViewResult)c.Details(_student.ID));
        }

        public void Then_the_details_view_is_displayed()
        {
            (SUT.ActionResult as ViewResult).ViewBag.Title = "Details";
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            SUT.Model.ID.Should().Be(1);
            SUT.Model.FirstMidName.Should().Be("Carson");
            SUT.Model.LastName.Should().Be("Alexander");
        }

        public void AndThen_the_enrollments_for_the_student_are_displayed()
        {
            SUT.Model.Enrollments.Count.Should().Be(3);
        }

        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}