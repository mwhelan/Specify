using System;
using Specify.Containers;

namespace Specify
{
    public abstract class SpecificationFor<TSut> : ISpecification
        where TSut : class
    {
        public TSut SUT { get; set; }
        public SpecificationContext<TSut> Context { get; set; }

        public virtual Type Story
        {
            get { return typeof(TSut); }
        }
        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public virtual void ExecuteTest()
        {
            Host.SpecificationRunner.Run(this);
        }

        protected virtual void EstablishContext()
        {
            SUT = Context.SystemUnderTest();
        }

        public T DependencyFor<T>()
        {
            return Context.DependencyFor<T>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            Context.InjectDependency(instance);
        }
    }
}