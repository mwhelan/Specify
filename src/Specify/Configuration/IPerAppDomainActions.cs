namespace Specify.Configuration
{
    public interface IPerAppDomainActions
    {
        void Before();
        void After();
    }
}