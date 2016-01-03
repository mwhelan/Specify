using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Configuration.Scanners;
using Specify.Mocks;

namespace Specify.Tests.Configuration.Scanners
{
    public class ConfigScannerFactoryTests
    {
        private readonly Type[] _scannerTypes = new []
        {
            typeof(SpecifyConfigScanner),
            typeof(SpecifyAutofacConfigScanner)
        };

        [Test]
        public void specify_without_plugin_should_return_SpecifyConfigurationScanner()
        {
            var result = SelectScanner(_scannerTypes.Take(1).ToArray());
            result.ShouldBeOfType<SpecifyConfigScanner>();
        }

        [Test]
        public void specify_with_plugin_should_return_plugin_ConfigurationScanner()
        {
            var result = SelectScanner(_scannerTypes);
            result.ShouldBeOfType<SpecifyAutofacConfigScanner>();
        }

        public IConfigScanner SelectScanner(Type[] types)
        {
            var fileSystem = Substitute.For<IFileSystem>();
            fileSystem.GetAllTypesFromAppDomain().Returns(types);
            return ConfigScannerFactory.SelectScanner(fileSystem);
        }

    }
}