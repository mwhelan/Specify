using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Containers.Mocking;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers.MockFactory
{
    [TestFixture]
    public abstract class MockFactoryTests<T> where T : IMockFactory, new()
    {
        public IMockFactory CreateSut()
        {
            return new T();
        }

        [Test]
        public void should_mock_interfaces()
        {
            var sut = CreateSut();
            var result = sut.CreateMock(typeof (IDependency1));

            result.ShouldNotBe(null);
            result.ShouldBeAssignableTo<IDependency1>();
        }

        [Test]
        public void should_mock_concrete()
        {
            var sut = CreateSut();
            var result = sut.CreateMock(typeof(Dependency1));

            result.ShouldNotBe(null);
            result.ShouldBeAssignableTo<Dependency1>();
        }
    }
}