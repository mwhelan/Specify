using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Mocks
{
    public class NSubstituteMockFactoryTests : MockFactoryTestsFor<NSubstituteMockFactory>
    {
        [Test]
        public void should_return_substitute()
        {
            var sut = CreateSut();
            var substitute = (IDependency1)sut.CreateMock(typeof(IDependency1));
            substitute.Value.Returns(11);

            var result = new ConcreteObjectWithOneInterfaceConstructor(substitute).Dependency1.Value;
            
            result.ShouldBe(11);
        }
    }
}
