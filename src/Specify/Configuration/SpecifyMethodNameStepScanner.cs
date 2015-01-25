using System;
using TestStack.BDDfy;

namespace Specify.Configuration
{
    public class SpecifyMethodNameStepScanner : DefaultMethodNameStepScanner
    {
        public SpecifyMethodNameStepScanner()
        {
            AddMatcher(new MethodNameMatcher(s => s.Equals("ConfigureContainer", StringComparison.OrdinalIgnoreCase), ExecutionOrder.Initialize) { ShouldReport = false });
            AddMatcher(new MethodNameMatcher(s => s.Equals("CreateSystemUnderTest", StringComparison.OrdinalIgnoreCase), ExecutionOrder.Initialize) { ShouldReport = false });
        }
    }

}
