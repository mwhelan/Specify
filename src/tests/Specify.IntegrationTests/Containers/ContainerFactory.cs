using System;
using Autofac;
using DryIoc;
using Specify.Autofac;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers
{
    public static class ContainerFactory
    {
        public static AutofacContainer CreateAutofacContainer<TMockFactory>(Action<ContainerBuilder> action = null)
        {
            action = action ?? (builder => { });

            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var builder = new ContainerBuilder();
            builder.RegisterSpecify(mockFactoryInstance, false);
            action(builder);
            var container = builder.Build();

            return new AutofacContainer(container);
        }

        public static DryContainer CreateDryContainer<TMockFactory>(Action<Container> action = null)
        {
            action = action ?? (builder => { });

            var mockFactoryInstance = typeof(TMockFactory).Create<IMockFactory>();
            var container = new DryContainerFactory().Create(mockFactoryInstance);
            action(container);

            return new DryContainer(container);
        }
    }
}