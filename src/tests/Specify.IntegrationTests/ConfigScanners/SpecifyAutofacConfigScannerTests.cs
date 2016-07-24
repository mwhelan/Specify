using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Configuration.Scanners;
using Specify.Mocks;

namespace Specify.IntegrationTests.ConfigScanners
{
    public class SpecifyAutofacConfigScannerTests
    {
        [Test]
        public void should_return_SpecifyAutofacBootstrapper_if_no_implementations()
        {
            var sut = CreateSut(new Type[] { });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultAutofacBootstrapper>();
        }

        [Test]
        public void should_not_return_abstract_class()
        {
            var sut = CreateSut(new[] { typeof(BootstrapperBase) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultAutofacBootstrapper>();
        }

        [Test]
        public void should_not_return_interface()
        {
            var sut = CreateSut(new[] { typeof(IBootstrapSpecify) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<DefaultAutofacBootstrapper>();
        }

        [Test]
        public void should_return_implementation_if_there_is_one()
        {
            var sut = CreateSut(new[] { typeof(DefaultAutofacBootstrapper), typeof(TestBootstrapper) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<TestBootstrapper>();
        }

        public ConfigScanner CreateSut(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return new SpecifyAutofacConfigScanner(fileSystem);
        }

        private class TestBootstrapper : DefaultAutofacBootstrapper { }
    }
}