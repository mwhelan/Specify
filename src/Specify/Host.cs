using System;
using Autofac;
using Specify.Configuration;
using TestStack.BDDfy;

namespace Specify
{
    internal static class Host
    {
        private static IContainer _container;
        private static SpecifyConfiguration _configuration;

        public static ISpecification PerformTest(ISpecification testObject)
        {
            InitializeSpecify(testObject);
            var specification = (ISpecification)_container.Resolve(testObject.GetType());
            specification.BDDfy(specification.Title);
            return specification;
        }

        private static void InitializeSpecify(ISpecification testObject)
        {
            if (_container != null)
            {
                return;
            }
            var configurator = new AppConfigurator(testObject);
            configurator.Configure();
            _container = configurator.Container;
            _configuration = configurator.Configuration;

            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.BeforeAllTests();
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _configuration.AfterAllTests();
        }
    }
}