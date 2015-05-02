using System;
using System.Web.Mvc;
using ContosoUniversity.AcceptanceTests.Infrastructure;
using ContosoUniversity.Controllers;
using ContosoUniversity.Domain.Model;
using FluentAssertions;
using MediatR;
using NUnit.Framework;
using Specify;

namespace ContosoUniversity.AcceptanceTests.Specifications
{
    public class MvcDriver //: ISystemUnderTest<TController>
    {
        public Controller Controller { get; set; }
        public ActionResult ActionResult { get; set; }
        public Exception Exception { get; set; }
        public object Model { get; set; }

        public virtual void ExecuteAction<TController>(Func<TController, ActionResult> func, TController controller)
        {
            //Controller.Url = new UrlHelper(new RequestContext(FakeHttpContext.Root(), new RouteData()), RouteTable.Routes);

            try
            {
                ActionResult = func(controller);
            }
            catch (Exception ex)
            {
                Exception = ex;
                if (Exception is NotImplementedException) throw ex;
            }
        }
    }
    public interface IPage<TModel>
    {
        TModel Model { get; set; }
    }

    public class StudentDetailsPage : IPage<Student>
    {
        public Student Model { get; set; }
        public string Title { get; set; }
    }
    public class StudentDetailsRequestHandler : IRequestHandler<StudentDetailsRequest, StudentDetailsPage>
    {
        public StudentDetailsPage Handle(StudentDetailsRequest message)
        {
            var controller = new ControllerScenario<StudentController, ActionResult, Student>();
            controller.ExecuteAction(c => c.Details(message.Id));
            var response = new StudentDetailsPage{Model = controller.Model};
            response.Title = (controller.ActionResult as ViewResult).ViewBag.Title;
            return response;
        }
    }
    public class StudentDetailsRequest : IRequest<StudentDetailsPage>
    {
        public int Id { get; set; }
    }
    public class ViewStudentDetailsScenarioWithMediator 
        : ScenarioFor<IMediator,StudentDetailsStory>
    {
        private StudentDetailsPage _response;
        private StudentDetailsRequest _request;
        public void Given_an_existing_student()
        {
            _request = new StudentDetailsRequest {Id = 1};
        }
        public void When_the_details_are_requested_for_that_Student()
        {
            _response = SUT.Send(_request);
        }

        public void Then_the_details_view_is_displayed()
        {
            _response.Title = "Details";
        }

        public void AndThen_the_details_are_of_the_requested_student()
        {
            _response.Model.Id.Should().Be(1);
            _response.Model.FirstMidName.Should().Be("Carson");
            _response.Model.LastName.Should().Be("Alexander");
        }

        public void AndThen_the_enrollments_for_the_student_are_displayed()
        {
            _response.Model.Enrollments.Count.Should().Be(3);
        }

        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}