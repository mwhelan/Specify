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
            result.ShouldBeOfType<SpecifyAutofacBootstrapper>();
        }

        [Test]
        public void should_not_return_abstract_class()
        {
            var sut = CreateSut(new[] { typeof(SpecifyConfiguration) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<SpecifyAutofacBootstrapper>();
        }

        [Test]
        public void should_not_return_interface()
        {
            var sut = CreateSut(new[] { typeof(IConfigureSpecify) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<SpecifyAutofacBootstrapper>();
        }

        [Test]
        public void should_return_implementation_if_there_is_one()
        {
            var sut = CreateSut(new[] { typeof(SpecifyAutofacBootstrapper), typeof(TestBootstrapper) });
            var result = sut.GetConfiguration();
            result.ShouldBeOfType<TestBootstrapper>();
        }

        public ConfigScanner CreateSut(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return new SpecifyAutofacConfigScanner(fileSystem);
        }

        private class TestBootstrapper : SpecifyAutofacBootstrapper { }
    }
}