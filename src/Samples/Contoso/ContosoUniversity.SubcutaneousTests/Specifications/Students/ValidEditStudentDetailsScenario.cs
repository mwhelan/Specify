using System;
using ContosoUniversity.Controllers;
using ContosoUniversity.DAL.Repositories;
using ContosoUniversity.Models;
using ContosoUniversity.SubcutaneousTests.Infrastructure;
using FluentAssertions;
using TestStack.FluentMVCTesting;

namespace ContosoUniversity.SubcutaneousTests.Specifications.Students
{
    public class ValidEditStudentDetailsScenario : ScenarioFor<MvcControllerDriver, StudentEditStory>
    {
        private Student _student;
        private ControllerResultTest<StudentController> _result;

        public void Given_I_am_editing_an_existing_student_with_valid_data()
        {
            _student = Container.Get<IStudentRepository>().FindById(1);
            _student.FirstMidName = "newFirstName";
            _student.LastName = "newLastName";
            _student.EnrollmentDate = new DateTime(2015,5,5);
        }
        public void When_I_save_the_changes()
        {
            _result = SUT.ExecuteActionFor<StudentController>(c => c.Edit(_student));
        }

        public void Then_I_am_returned_to_the_student_list()
        {
            _result.ShouldRedirectTo<StudentController>(c => c.Index(null, null, null, null));
        }

        public void AndThen_the_changes_have_been_saved()
        {
            Container.Get<IStudentRepository>()
                .FindById(1)
                .ShouldBeEquivalentTo(_student);
        }
    }
}