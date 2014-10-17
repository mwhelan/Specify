using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Specify.Containers;

namespace Specify.Configuration
{
    public class SpecifyConfiguration
    {
        public HtmlReportConfiguration HtmlReport { get; protected set; }

        public SpecifyConfiguration()
        {
            HtmlReport = new HtmlReportConfiguration();
        }

        public virtual void BeforeAllTests()
        {
        }

        public virtual void AfterAllTests()
        {
        }

        public virtual IDependencyResolver DependencyResolver()
        {
            return new NSubstituteDependencyResolver();
        }

        public virtual ITestContainer TestContainer()
        {
            return BuildTestContainer();
        }

        public virtual IEnumerable<Type> ScanForSpecificationTypes()
        {
            return AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(t => typeof(ISpecification).IsAssignableFrom(t));
        }

        private ITestContainer BuildTestContainer()
        {
            var builder = new ContainerBuilder();

            foreach (var specification in ScanForSpecificationTypes())
            {
                builder.RegisterType(specification)
                    .PropertiesAutowired();
            }

            builder.RegisterType(DependencyResolver().GetType()).As<IDependencyResolver>();
            builder.RegisterGeneric(typeof (SpecificationContext<>));
            var container = builder.Build();

            return new AutofacTestContainer(container);
        }

    }
}
