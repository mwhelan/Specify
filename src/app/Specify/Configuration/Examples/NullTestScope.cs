using Specify.Containers;

namespace Specify.Configuration.Examples
{
    class NullTestScope : TestScope
    {
        public IChildContainerBuilder Registrations { get; }

        public NullTestScope()
            : base(null) { }

        public override void BeginScope<T>(IScenario<T> scenario) 
        {
            
        }

        public override void EndScope<T>(IScenario<T> scenario) 
        {
            
        }
    }
}