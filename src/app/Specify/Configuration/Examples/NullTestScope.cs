using Specify.Containers;

namespace Specify.Configuration.Examples
{
    class NullTestScope : ITestScope
    {
        public IChildContainerBuilder Registrations { get; }

        public void BeginScope<T>(IScenario<T> scenario) where T : class
        {
            
        }

        public void EndScope<T>(IScenario<T> scenario) where T : class
        {
            
        }
    }
}