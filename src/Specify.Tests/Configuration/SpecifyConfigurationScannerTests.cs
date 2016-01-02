using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Mocks;

namespace Specify.Tests.Configuration
{
    public class SpecifyConfigurationScannerTests
    {
        [Test]
        public void should_return_SpecifyBootstrapper_if_no_implementations()
        {
            var sut = CreateSut(new Type[] {});
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<SpecifyBootstrapper>();
        }

        [Test]
        public void should_not_return_abstract_class()
        {
            var sut = CreateSut(new[] { typeof(SpecifyConfiguration) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<SpecifyBootstrapper>();
        }

        [Test]
        public void should_not_return_interface()
        {
            var sut = CreateSut(new[] { typeof(IConfigureSpecify) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<SpecifyBootstrapper>();
        }

        [Test]
        public void should_return_implementation_if_there_is_one()
        {
            var sut = CreateSut(new [] { typeof (SpecifyBootstrapper), typeof(TestBootstrapper) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<TestBootstrapper>();
        }

        public ConfigurationScanner CreateSut(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return new SpecifyConfigurationScanner(fileSystem);
        }

        private class TestBootstrapper : SpecifyBootstrapper{}
    }
}
