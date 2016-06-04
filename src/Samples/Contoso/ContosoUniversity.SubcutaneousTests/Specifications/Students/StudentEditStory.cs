using Specify.Stories;

namespace ContosoUniversity.SubcutaneousTests.Specifications.Students
{
    public class StudentEditStory : ValueStory
    {
        public StudentEditStory()
        {
            InOrderTo = "change details for a Student";
            AsA = "Student Administrator";
            IWant = "to edit the details of a Student";
        }
    }
}