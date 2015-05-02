using System.Collections.Generic;

namespace ContosoUniversity.ViewModels
{
    public class InstructorIndexDataViewModel
    {
        public IEnumerable<InstructorViewModel> Instructors { get; set; }
        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}