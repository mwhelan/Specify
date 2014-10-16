using TestStack.BDDfy;

namespace Specify
{
    internal class BddfyTestEngine : ITestEngine
    {
        public void Execute(object testObject, string scenarioTitle = null)
        {
            testObject.BDDfy(scenarioTitle);
        }
    }
}