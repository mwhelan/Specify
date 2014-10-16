namespace Specify
{
    public interface ITestEngine
    {
        void Execute(object testObject, string scenarioTitle = null);
    }
}