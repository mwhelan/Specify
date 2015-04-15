using FluentAssertions;
using NUnit.Framework;
using Specify;

namespace ContosoUniversity.FunctionalTests.Specifications
{
    using ContosoUniversity.Controllers;
    using ContosoUniversity.Models;

    using MediatR;

    public class StudentDetailsRequestHandler : IRequestHandler<StudentDetailsRequest, IPage<Student>>
    {
        private readonly BrowserHost _browser;

        public StudentDetailsRequestHandler(BrowserHost browser)
        {
            _browser = browser;
        }

        public IPage<Student> Handle(StudentDetailsRequest message)
        {
            return _browser.Host.NavigateToInitialPage<StudentController, StudentDetailsPage>(
                c => c.Details(message.Id));
        }
    }
    public class StudentDetailsRequest : IRequest<StudentDetailsPage>
    {
        public int Id { get; set; }
    }

    public class ViewStudentDetailsScenarioWithMediator
            : ScenarioFor<IMediator, StudentDetailsStory>
    {
        private StudentDetailsPage _response;
        private StudentDetailsRequest _request;
        public void Given_an_existing_student()
        {
            _request = new StudentDetailsRequest { Id = 1 };
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _response = SUT.Send(_request);
        }

        public void Then_the_details_view_is_displayed()
        {
            _response.Title.Should().Be("Details");
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            var model = _response.GetModel();
            model.ID.Should().Be(1);
            model.FirstMidName.Should().Be("Carson");
            model.LastName.Should().Be("Alexander");
        }

        public void AndThen_the_enrollments_for_the_student_are_displayed()
        {
            var model = _response.GetModel();
            model.Enrollments.Count.Should().Be(3);
        }

        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}