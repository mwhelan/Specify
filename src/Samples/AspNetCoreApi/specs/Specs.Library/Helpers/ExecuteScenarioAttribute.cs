using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace Specs.Library.Helpers
{
    /// <summary>
    /// Sets the NUnit TestName to the name of the scenario class.
    /// By default, NUnit would set every test with the name of the method ('Specify')
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExecuteScenarioAttribute : NUnitAttribute, ISimpleTestBuilder
    {
        public TestMethod BuildFrom(IMethodInfo method, Test suite)
        {
            var builder = new NUnitTestCaseBuilder();
            var testMethod = builder.BuildTestMethod(method, suite, null);
            testMethod.Name = method.TypeInfo.Name;
            return testMethod;
        }
    }
}