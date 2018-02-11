using System;
using TestStack.BDDfy;

namespace Specify.Configuration.ExecutableAttributes
{
    /// <summary>
    /// Will run after every BDDfy method for each test case (scenario or example)
    /// </summary>
    public class EndTestCaseAttribute : ExecutableAttribute
    {
        public EndTestCaseAttribute() : base(ExecutionOrder.TearDown, null)
        {
            ShouldReport = false;
            Order = int.MaxValue;   // Forces running after any other ExecutionOrder.TearDown method
        }
    }
}
