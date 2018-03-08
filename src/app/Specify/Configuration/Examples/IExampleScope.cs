namespace Specify.Configuration.Examples
{
    public interface IExampleScope
    {
        void BeginScope<T>(IScenario<T> scenario)
            where T : class;

        void EndScope<T>(IScenario<T> scenario)
            where T : class;
    }
}