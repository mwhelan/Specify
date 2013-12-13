using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using AutofacContrib.NSubstitute;

namespace Specify.Containers
{
    public class AutoMockingContainer<TSut> 
    {
        public TSut SUT { get { return AutoSubstitute.Resolve<TSut>(); } }
        protected AutoSubstitute AutoSubstitute;

        public AutoMockingContainer()
        {
            Action<ContainerBuilder> autofacCustomisation = c => c
                .RegisterType<TSut>()
                .FindConstructorsWith(
                    t => t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                .PropertiesAutowired();
            AutoSubstitute = new AutoSubstitute(autofacCustomisation);
        }

        public T Resolve<T>(params Parameter[] parameters)
        {
            return AutoSubstitute.Resolve<T>();
        }

        public TSubstitute SubFor<TSubstitute>() where TSubstitute : class
        {
            return AutoSubstitute.ResolveAndSubstituteFor<TSubstitute>();
        }
    }
}