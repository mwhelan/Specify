using TestStack.Seleno.Configuration;

namespace ContosoUniversity.FunctionalTests
{
    public class BrowserHost
    {
        public SelenoHost Host { get; private set; }

        public BrowserHost(SelenoHost host)
        {
            Host = host;
        }
    }
}