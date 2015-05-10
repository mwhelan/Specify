namespace Specify.Configuration
{
    public interface IPerAppDomainActions
    {
        void Before(IApplicationContainer container);
        void After();
    }
}