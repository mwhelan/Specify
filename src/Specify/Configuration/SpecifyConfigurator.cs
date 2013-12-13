using System;
using Specify.Containers;

namespace Specify.Configuration
{
    public static class SpecifyConfigurator
    {
        private static ISpecifyConfig _environment;

        public static void Initialize()
        {
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _environment = new StartupScanner().GetTestLifecycle();
            _environment.BeforeAllTests();
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _environment.AfterAllTests();
        }

        public static IDependencyLifetime GetDependencyResolver()
        {
            return _environment.GetChildContainer().Invoke();
        }
    }
}