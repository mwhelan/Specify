using Specify.Stories;

namespace ContosoUniversity.FunctionalTests.Specifications
{
    public class StudentDetailsStory : UserStory
    {
        public StudentDetailsStory()
        {
            AsA = "Student Administrator";
            IWant = "View the details of a Student";
            SoThat = "I can see what grade she achieved";
        }
    }
}