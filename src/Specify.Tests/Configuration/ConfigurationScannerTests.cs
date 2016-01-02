using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Mocks;

namespace Specify.Tests.Configuration
{
    public class ConfigurationScannerTests
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

        //[Test]
        //public void should_return_SpecifyAutofacBootstrapper_if_there_is_one()
        //{
        //    var sut = CreateSut(new[] { typeof(SpecifyBootstrapper), typeof(TestBootstrapper), typeof(SpecifyAutofacBootstrapper) });
        //    var result = sut.GetConfiguration();
        //    result.ShouldBeOfType<SpecifyAutofacBootstrapper>();
        //}

        public ConfigurationScanner CreateSut(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return new ConfigurationScanner(fileSystem);
        }

        private class TestBootstrapper : SpecifyBootstrapper{}
    }
}
