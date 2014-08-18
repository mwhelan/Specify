using System;
using System.Reflection;
using System.Runtime.Remoting.Services;
using Autofac;
using Autofac.Core;
using AutofacContrib.NSubstitute;

namespace Specify.Core
{
    public abstract class SpecificationFor<T> : Specification
    {
        public T SUT { get; set; }
        protected AutoSubstitute Container;

        protected SpecificationFor()
        {
            Container = CreateContainer();
            InitialiseSystemUnderTest();
        }

        public virtual void InitialiseSystemUnderTest()
        {
             SUT = Container.Resolve<T>();
        }

        public TSubstitute SubFor<TSubstitute>() where TSubstitute : class
        {
            return Container.ResolveAndSubstituteFor<TSubstitute>();
        }

        public TService Resolve<TService>(params Parameter[] parameters)
        {
            return Container.Resolve<TService>(parameters);
        }

        public override Type Story
        {
            get { return typeof(T); }
        }

        private static AutoSubstitute CreateContainer()
        {
            Action<ContainerBuilder> autofacCustomisation = c => c
                .RegisterType<T>()
                .FindConstructorsWith(t =>  t.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                .PropertiesAutowired();
            return new AutoSubstitute(autofacCustomisation);
        }
    }

}
