using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration;
using Specify.Configuration.Scanners;
using Specify.Mocks;

namespace Specify.Tests.Configuration.Scanners
{
    public class SpecifyConfigScannerTests
    {
        [Test]
        public void should_return_SpecifyBootstrapper_if_no_implementations()
        {
            var sut = CreateSut(new Type[] {});
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultBootstrapper>();
        }

        [Test]
        public void should_not_return_abstract_class()
        {
            var sut = CreateSut(new[] { typeof(BootstrapperBase) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultBootstrapper>();
        }

        [Test]
        public void should_not_return_interface()
        {
            var sut = CreateSut(new[] { typeof(IBootstrapSpecify) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultBootstrapper>();
        }

        [Test]
        public void should_return_implementation_if_there_is_one()
        {
            var sut = CreateSut(new [] { typeof (DefaultBootstrapper), typeof(TestBootstrapper) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<TestBootstrapper>();
        }

        public ConfigScanner CreateSut(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return new SpecifyConfigScanner(fileSystem);
        }

        private class TestBootstrapper : DefaultBootstrapper{}
    }
}
