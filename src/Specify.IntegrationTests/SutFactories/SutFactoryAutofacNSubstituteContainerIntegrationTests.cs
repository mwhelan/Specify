namespace Specify.IntegrationTests.SutFactories
{
    using Specify.Autofac;
    using Specify.Mocks;

    public class SutFactoryAutofacNSubstituteContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var container = new AutoMockingContainer(new NSubstituteMockFactory());
            return new SutFactory<T>(container);
        }
    }
}