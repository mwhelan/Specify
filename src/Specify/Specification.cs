using System;
using Specify.Containers;

namespace Specify
{
    //public interface ISpecificationFor<TSut> : ISpecification<TSut, AutoMockingContainer<TSut>> where TSut : class{}
    //public interface ISpecification<TSut, TResolver> : ISpecification where TSut : class
    //{
    //    TSut SUT { get; set; }
    //    TResolver Resolver { get; set; }
    //}

    public interface ISpecification : IDisposable
    {
        ISpecification ExecuteTest();
        T DependencyFor<T>();
        void InjectDependency<TService>(TService instance) where TService : class;
        Type Story { get; }
        string Title { get; }
    }

    public abstract class Specification<TSut, TResolver> : ISpecification//<TSut,TResolver> 
        where TSut : class
        where TResolver : ITestContainer<TSut>
    {
        public TSut SUT { get; set; }
        public TResolver Resolver { get; set; }

        public virtual Type Story
        {
            get { return typeof(TSut); }
        }
        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public ISpecification ExecuteTest()
        {
            return this.Specify();
        }

        protected virtual void EstablishContext()
        {
            SUT = Resolver.SystemUnderTest();
        }

        public T DependencyFor<T>()
        {
            return Resolver.DependencyFor<T>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            Resolver.InjectDependency(instance);
        }

        public void Dispose()
        {
            Resolver.Dispose();
        }
    }
}
