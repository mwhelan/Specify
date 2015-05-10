namespace Specify.Configuration
{
    public interface IPerScenarioActions
    {
        void Before(IScenarioContainer container);
        void After();
    }
}
