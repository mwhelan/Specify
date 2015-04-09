using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Containers.Mocking;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers.MockFactory
{
    public class FakeItEasyMockFactoryTests : MockFactoryTests<FakeItEasyMockFactory>
    {
        [Test]
        public void should_return_fake()
        {
            var sut = CreateSut();
            var fake = (IDependency1)sut.CreateMock(typeof(IDependency1));

            A.CallTo(() => fake.Value).Returns(11);

            var result = new ConcreteObjectWithOneInterfaceConstructor(fake).Dependency1.Value;

            result.ShouldBe(11);
        }
    }
}