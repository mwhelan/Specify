using System;
using Specify.Autofac;
using Specify.IntegrationTests.SutFactories.Ioc;
using Specify.Mocks;

namespace Specify.IntegrationTests.SutFactories.AutoMocking
{
    public class AutofacFakeItEasyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            Func<IMockFactory> mockFactory = () => new FakeItEasyMockFactory();
            var container = new AutofacContainerFactory().Create(mockFactory).Build();
            return new ContainerFor<T>(new AutofacContainer(container));
        }
    }
}
