using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Domain.Model
{
    public class Enrollment : IEntity
    {
        public int Id { get; set; }
        public int CourseId => Course.Id;
        public int StudentId => Student.Id;

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