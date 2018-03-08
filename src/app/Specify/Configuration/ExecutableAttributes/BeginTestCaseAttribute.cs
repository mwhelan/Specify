using TestStack.BDDfy;

namespace Specify.Configuration.ExecutableAttributes
{
    /// <summary>
    /// Will run before every BDDfy method for each test case (scenario or example)
    /// </summary>
    public class BeginTestCaseAttribute : ExecutableAttribute
    {
        public BeginTestCaseAttribute() : base(ExecutionOrder.Initialize, null)
        {
            ShouldReport = false;
            Order = int.MinValue;   // Forces running before any other ExecutionOrder.Initialize method
        }
    }
}