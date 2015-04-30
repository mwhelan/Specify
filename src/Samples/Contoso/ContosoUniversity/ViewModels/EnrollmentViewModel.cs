using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }
        public int CourseId { get { return CourseViewModel.Id; } }
        public int StudentId { get { return StudentViewModel.Id; } }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public virtual CourseViewModel CourseViewModel { get; set; }
        public virtual StudentViewModel StudentViewModel { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Course: {1}, Student: {2}",
                Id, CourseViewModel.Title, StudentViewModel.FullName);
        }
    }
}