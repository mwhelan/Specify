using Specify.Containers;

namespace Specify.Configuration.Examples
{
    class NullTestScope : TestScope
    {
        public IChildContainerBuilder Registrations { get; }

        public NullTestScope()
            : base(null) { }

        internal override void BeginScope<T>(IScenario<T> scenario) 
        {
            
        }

        internal override void EndScope<T>(IScenario<T> scenario) 
        {
            
        }
    }
}