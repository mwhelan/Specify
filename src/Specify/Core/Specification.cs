using System;
using Specify.Containers;
using TestStack.BDDfy;

namespace Specify.Core
{
    public abstract class Specification<TSut, TContainer> : ISpecification, IDisposable
        where TSut : class 
        where TContainer : ISutResolver<TSut> 
    {
        public TSut SUT { get; set; }
        protected TContainer Container;

        protected Specification(TContainer container)
        {
            Container = container;
        }

        public virtual void Run()
        {
            this.BDDfy(Title);
        }

        public virtual void EstablishContext()
        {
            InitialiseSystemUnderTest();
        }

        public virtual void InitialiseSystemUnderTest()
        {
            SUT = Container.Resolve<TSut>();
        }

        public TService DependencyFor<TService>()
        {
            return Container.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            Container.Inject(instance);
        }

        public virtual Type Story
        {
            get { return typeof(TSut); }
        }
        public virtual string Title { get; set; }
        public string Category { get; set; }
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}