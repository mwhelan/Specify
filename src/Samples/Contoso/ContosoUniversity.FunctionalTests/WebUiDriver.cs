using TestStack.Seleno.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    public class WebUiDriver
    {
        public SelenoHost Host { get; private set; }

        public WebUiDriver(SelenoHost host)
        {
            Host = host;
        }
    }
}