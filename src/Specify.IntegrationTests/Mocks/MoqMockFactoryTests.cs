using Moq;
using NUnit.Framework;
using Shouldly;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Mocks
{
    public class MoqMockFactoryTests : MockFactoryTestsFor<MoqMockFactory>
    {
        [Test]
        public void should_return_mock()
        {
            var sut = CreateSut();
            var mock = (IDependency1)sut.CreateMock(typeof(IDependency1));
            Mock.Get(mock).Setup(x => x.Value).Returns(11);

            var result = new ConcreteObjectWithOneInterfaceConstructor(mock).Dependency1.Value;

            result.ShouldBe(11);
        }
    }
}