using System;
using Specify.Containers;

namespace Specify
{
    public abstract class SpecificationFor<TSut> : ISpecification
        where TSut : class
    {
        private SpecificationContext<TSut> _context;
        public TSut SUT { get; set; }
        public IDependencyScope Scope { get; set; }

        public virtual Type Story
        {
            get { return typeof(TSut); }
        }
        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        internal SpecificationContext<TSut> Context
        {
            get
            {
                if(_context == null)
                    _context = new SpecificationContext<TSut>(Scope);
                return _context;
            }
        }

        public virtual ISpecification ExecuteTest()
        {
            return Host.SpecificationRunner.Run(this);
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

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}