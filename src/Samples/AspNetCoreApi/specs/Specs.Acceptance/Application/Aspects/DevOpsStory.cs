using Specify.Stories;

namespace Specs.Acceptance.Application.Aspects
{
    public class DevOpsStory : UserStory
    {
        public DevOpsStory()
        {
            AsA = "As a DevOps person";
            IWant = "I want to check the status of the application";
            SoThat = "So that I can operate and monitor the software for the users.";
        }
    }
}