using System;
using ContosoUniversity.Controllers;
using ContosoUniversity.DAL.Repositories;
using ContosoUniversity.Models;
using ContosoUniversity.SubcutaneousTests.Builders;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using FluentAssertions;
using TestStack.FluentMVCTesting;

namespace ContosoUniversity.SubcutaneousTests.Specifications.Students
{
    //public class InValidEditStudentDetailsScenario : ScenarioFor<MvcControllerDriver, StudentEditStory>
    //{
    //    private Student _student;
    //    private ControllerResultTest<StudentController> _result;

    //    public void Given_I_am_editing_an_existing_student_with_invalid_data()
    //    {
    //        _student = new StudentBuilder()
    //            .WithId(1)
    //            .WithFirstName(string.Empty)
    //            .WithLastName(string.Empty);
    //    }
    //    public void When_I_save_the_changes()
    //    {
    //        _result = SUT.ExecuteActionFor<StudentController>(c => c.Edit(_student));
    //    }

    //    public void Then_I_should_stay_on_the_edit_page()
    //    {
    //        _result.ShouldRenderDefaultView();
    //    }

    //    public void AndThen_should_receive_notification_of_the_errors()
    //    {
    //        _result.ShouldRenderDefaultView()
    //            .WithModel<Student>()
    //            .AndModelErrorFor(x => x.FirstMidName)
    //            .AndModelErrorFor(x => x.LastName);
    //    }
    //}
}