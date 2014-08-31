using Autofac;
using Specify.Configuration;
using TestStack.BDDfy;

namespace Specify
{
    public static class SpecifyExtensions
    {
        public static void Specify(this object testObject, string scenarioTitle = null)
        {
            var spec = GetTestObject(testObject);
            spec.BDDfy(scenarioTitle);
        }

        private static object GetTestObject(object testObject)
        {
            if (SpecifyConfigurator.Container == null)
            {
                SpecifyConfigurator.Initialize(testObject);
            }
            return SpecifyConfigurator.Container.Resolve(testObject.GetType());
        }
    }
}
