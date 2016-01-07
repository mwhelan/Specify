using System;
using Specify.Configuration;
using Specify.Mocks;
using TinyIoC;

namespace Specify.IntegrationTests.ContainerFors.AutoMocking
{
    public class TinyNSubstituteContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyMockingContainer(new NSubstituteMockFactory(), new TinyIoCContainer());
            return new ContainerFor<T>(container);
        }
    }

    public class TinyMoqContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyMockingContainer(new MoqMockFactory(), new TinyIoCContainer());
            return new ContainerFor<T>(container);
        }
    }

    public class TinyFakeItEasyContainerForIntegrationTests : ContainerForIntegrationTestsBase
    {
        protected override ContainerFor<T> CreateSut<T>()
        {
            var container = new TinyMockingContainer(new FakeItEasyMockFactory(), new TinyIoCContainer());
            return new ContainerFor<T>(container);
        }
    }
}
