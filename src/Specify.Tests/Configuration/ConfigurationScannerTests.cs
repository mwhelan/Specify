using System;
using System.Linq;
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
        private readonly Type[] _scannerTypes = new []
        {
            typeof(SpecifyConfigurationScanner),
            typeof(SpecifyAutofacConfigurationScanner)
        };

        [Test]
        public void specify_alone_should_return_SpecifyConfigurationScanner()
        {
            var result = FindScanner(_scannerTypes.Take(1).ToArray());
            result.ShouldBeOfType<SpecifyConfigurationScanner>();
        }

        [Test]
        public void specify_with_plugin_should_return_plugin_ConfigurationScanner()
        {
            var result = FindScanner(_scannerTypes);
            result.ShouldBeOfType<SpecifyAutofacConfigurationScanner>();
        }

        public IConfigurationScanner FindScanner(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return ConfigurationScanner.FindScanner(fileSystem);
        }

    }
}