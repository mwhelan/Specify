using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.ViewModels
{
    public class OfficeAssignmentViewModel
    {
        [Key]
        [ForeignKey("Instructor")]
        public int InstructorId { get { return InstructorViewModel.Id; } }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual InstructorViewModel InstructorViewModel { get; set; }
    }
}