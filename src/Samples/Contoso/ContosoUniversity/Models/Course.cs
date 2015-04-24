using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Course : IEntity
    {
        public Course()
        {
            Enrollments = new List<Enrollment>();
            Instructors = new List<Instructor>();
        }

        [Display(Name = "Number")]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentId { get { return Department.Id; } }

        public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}