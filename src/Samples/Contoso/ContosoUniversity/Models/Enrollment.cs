using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Enrollment : IEntity
    {
        public int Id { get; set; }
        public int CourseId { get { return Course.Id; } }
        public int StudentId { get { return Student.Id; } }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Course: {1}, Student: {2}",
                Id, Course.Title, Student.FullName);
        }
    }
}