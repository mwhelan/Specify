using Specify.Stories;

namespace ContosoUniversity.SubcutaneousTests.Specifications
{
    public class StudentDetailsStory : ValueStory
    {
        public StudentDetailsStory()
        {
            InOrderTo = "see what grade a student received";
            AsA = "Student Administrator";
            IWant = "view the details of a Student";
        }
    }
}