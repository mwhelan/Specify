using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.ViewModels
{
    public class DepartmentViewModel 
    {
        public DepartmentViewModel()
        {
            Courses = new List<CourseViewModel>();
        }
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorId => InstructorViewModel.Id;

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual InstructorViewModel InstructorViewModel { get; set; }
        public virtual ICollection<CourseViewModel> Courses { get; set; }
    }
}