using System;
using System.Reflection;
using Autofac;
using AutofacContrib.NSubstitute;

namespace Specify.Containers
{
    public class AutoSubResolver<TSut> : ISutResolver<TSut> where TSut : class
    {
        private TSut _systemUnderTest;
        private readonly AutoSubstitute _autoSubstitute;

        public AutoSubstitute Container { get { return _autoSubstitute; }}

        public AutoSubResolver()
        {
            _autoSubstitute = CreateContainer();
        }

        public TService Resolve<TService>()
        {
            return _autoSubstitute.Resolve<TService>();
        }

        public void Inject<TService>(TService instance) where TService : class
        {
            _autoSubstitute.Provide(instance);
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = _autoSubstitute.Resolve<TSut>();
            }
            return _systemUnderTest;
        }

        private static AutoSubstitute CreateContainer()
        {
            Action<ContainerBuilder> autofacCustomisation = c => c
                .RegisterType<TSut>()
                .FindConstructorsWith(t => t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                .PropertiesAutowired();
            return new AutoSubstitute(autofacCustomisation);
        }

        public void Dispose()
        {
            _autoSubstitute.Dispose();
        }
    }
}
