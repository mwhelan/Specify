using System;
using System.IO;
using System.Reflection;
using FakeItEasy;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Mocks
{
    public class FakeItEasyMockFactoryTests : MockFactoryTestsFor<FakeItEasyMockFactory>
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