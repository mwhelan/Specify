using System;
using Specify.Containers;
using Specify.Stories;
using TestStack.BDDfy;

namespace Specify
{
    public abstract class SpecificationFor<TSubject> 
        : SpecificationFor<TSubject, SpecificationStory> where TSubject : class { }

    public abstract class SpecificationFor<TSubject, TStory> : ISpecification
        where TSubject : class
        where TStory : Stories.Story
    {
        public SutFactory Container { get; set; }
        public ExampleTable Examples { get; set; }

        public TSubject SUT { get; set; }

        public T Get<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            return Container.RegisterInstance(valueToSet, key);
        }

        public virtual Type Story
        {
            get { return typeof(TStory); }
        }

        public virtual string Title { get { return GetType().Name.Humanize(LetterCasing.Title); } }

        public virtual void ExecuteTest()
        {
            Host.Specify(this);
        }

        [Executable(ExecutionOrder.Initialize, "", Order = -2, ShouldReport = false)]
        protected virtual void ConfigureContainer()
        {
        }

        [Executable(ExecutionOrder.Initialize, "", Order = -1, ShouldReport = false)]
        protected virtual void CreateSystemUnderTest()
        {
            SUT = Container.SystemUnderTest<TSubject>();            
        }
    }
}