using Specify.Containers;

namespace Specify.Configuration.Examples
{
    public interface ITestScope
    {
        IChildContainerBuilder Registrations { get; }

        void BeginScope<T>(IScenario<T> scenario)
            where T : class;

        void EndScope<T>(IScenario<T> scenario)
            where T : class;
    }
}