using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers
{
    public static class ContainerFactory
    {
        public static AutofacContainer CreateAutofacContainer<TMockFactory>()
        {
            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var builder = new ContainerBuilder();
            builder.RegisterSpecify(mockFactoryInstance);
            var container = builder.Build();

            return new AutofacContainer(container);
        }
    }
}
