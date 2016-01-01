using System;
using System.Collections.Generic;
using Autofac;
using Specify.Configuration;
using Specify.Mocks;
using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Autofac
{
    public class SpecifyAutofacBootstrapper : SpecifyBootstrapperBase
    {
        protected override IContainer BuildApplicationContainer()
        {
            var mockFactory = GetMockFactory();
            var builder = new AutofacContainerFactory().Create(mockFactory);
            ConfigureContainer(builder);
            var container = builder.Build();
            return new AutofacContainer(container);
        }

        public virtual void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}