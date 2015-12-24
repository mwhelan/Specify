namespace Specify.Configuration
{
    public interface IPerScenarioActions
    {
        void Before(IContainer container);
        void After();
    }
}
