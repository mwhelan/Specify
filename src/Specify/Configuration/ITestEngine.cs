using TestStack.BDDfy;

namespace Specify.Configuration
{
    public interface ITestEngine
    {
        Story Execute(IScenario scenario);
    }
}