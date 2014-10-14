using System;
using Specify.Containers;

namespace Specify
{
    public abstract class SpecificationFor<TSut> : ISpecification
        where TSut : class
    {
        private readonly SpecificationContext<TSut> _context;
        public TSut SUT { get; set; }
        internal IDependencyScope Scope { get; set; }

        protected SpecificationFor()
        {
            _context = new SpecificationContext<TSut>(Scope);
        }

        public virtual Type Story
        {
            get { return typeof(TSut); }
        }
        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public virtual ISpecification ExecuteTest()
        {
            return this.Specify();
        }

        protected virtual void EstablishContext()
        {
            SUT = _context.SystemUnderTest();
        }

        public T DependencyFor<T>()
        {
            return _context.DependencyFor<T>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            _context.InjectDependency(instance);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}