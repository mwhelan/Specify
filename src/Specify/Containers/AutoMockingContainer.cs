using System;
using System.Reflection;
using Autofac;
using AutofacContrib.NSubstitute;

namespace Specify.Containers
{
    public class AutoMockingContainer<TSut> : ITestContainer<TSut> where TSut : class
    {
        public AutoSubstitute Container { get; private set; }

        public AutoMockingContainer()
        {
            Container = CreateContainer();
        }

        public AutoMockingContainer(Action<ContainerBuilder> autofacCustomisation)
        {
            Container = new AutoSubstitute(autofacCustomisation);
        }

        public TService DependencyFor<TService>()
        {
            return Container.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot inject dependencies after the System Under Test has been created");
            }
            Container.Provide(instance);
        }

        private TSut _systemUnderTest;
        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = Container.Resolve<TSut>();
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
            Container.Dispose();
        }
    }
}