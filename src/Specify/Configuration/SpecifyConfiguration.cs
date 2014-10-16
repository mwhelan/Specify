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

        public virtual Func<IDependencyScope> DependencyFactory()
        {
            return () => new NSubstituteDependencyScope();
        }

        public virtual IDependencyResolver DependencyResolver()
        {
            return CreateDependencyResolver();
        }

        public virtual IEnumerable<Type> ScanForSpecificationTypes()
        {
            return AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(t => typeof(ISpecification).IsAssignableFrom(t));
        }

        private IDependencyResolver CreateDependencyResolver()
        {
            var builder = new ContainerBuilder();

            foreach (var specification in ScanForSpecificationTypes())
            {
                builder.RegisterType(specification)
                    .PropertiesAutowired();
            }

            builder.Register<Func<IDependencyScope>>(c => DependencyFactory());
            //builder.Register<IDependencyResolver>(c => new AutofacDependencyResolver(c.Resolve<IContainer>())).SingleInstance();
            var container = builder.Build();

            return new AutofacDependencyResolver(container);
        }

    }
}
