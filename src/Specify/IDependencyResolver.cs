namespace Specify
{
    public interface IDependencyResolver : IScenarioContainer
    {
        IScenarioContainer CreateChildContainer();
    }
}
