using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels
{
    public class CourseViewModel 
    {
        public CourseViewModel()
        {
            Enrollments = new List<EnrollmentViewModel>();
            Instructors = new List<InstructorViewModel>();
        }

        [Display(Name = "Number")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentId { get; set; }

        public virtual DepartmentViewModel DepartmentViewModel { get; set; }
        public virtual ICollection<EnrollmentViewModel> Enrollments { get; set; }
        public virtual ICollection<InstructorViewModel> Instructors { get; set; }
    }
}