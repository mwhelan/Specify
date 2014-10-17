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

        public virtual Func<IDependencyResolver> DependencyFactory()
        {
            return () => new NSubstituteDependencyResolver();
        }

        public virtual ITestContainer DependencyResolver()
        {
            return CreateDependencyResolver();
        }

        public virtual IEnumerable<Type> ScanForSpecificationTypes()
        {
            return AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(t => typeof(ISpecification).IsAssignableFrom(t));
        }

        private ITestContainer CreateDependencyResolver()
        {
            var builder = new ContainerBuilder();

            foreach (var specification in ScanForSpecificationTypes())
            {
                builder.RegisterType(specification)
                    .PropertiesAutowired();
            }

            //builder.Register<Func<IDependencyResolver>>(c => DependencyFactory());
            //builder.Register<ITestContainer>(c => new AutofacTestContainer(c.Resolve<IContainer>())).SingleInstance();
            builder.RegisterType<NSubstituteDependencyResolver>().As<IDependencyResolver>();
            builder.RegisterGeneric(typeof (SpecificationContext<>));
            var container = builder.Build();

            return new AutofacTestContainer(container);
        }

    }
}
