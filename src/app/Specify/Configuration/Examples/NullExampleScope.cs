namespace Specify.Configuration.Examples
{
    class NullExampleScope : IExampleScope
    {
        public void BeginScope<T>(IScenario<T> scenario) where T : class
        {
            
        }

        public void EndScope<T>(IScenario<T> scenario) where T : class
        {
            
        }
    }
}