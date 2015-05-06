namespace Specify
{
    public interface IApplicationContainer : IScenarioContainer
    {
        IScenarioContainer CreateChildContainer();
    }
}
