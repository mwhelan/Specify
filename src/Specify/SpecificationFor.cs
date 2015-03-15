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
        where TStory : Stories.Story, new()
    {
        public SutFactory Container { get; internal set; }
        public ExampleTable Examples { get; set; }

        public TSubject SUT
        {
            get { return Container.SystemUnderTest<TSubject>(); }
            set { Container.SetSystemUnderTest<TSubject>(value); }
        }

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

        public virtual string Title
        {
            get
            {
                if (Story.Name == "SpecificationStory")
                {
                    return typeof (TSubject).Name;
                }
                else
                {
                    var title = GetType().Name.Humanize(LetterCasing.Title);
                    if (Number != 0)
                    {
                        title = string.Format("Scenario {0}: {1}", Number.ToString("00"), title);
                    }
                    return title;
                }
            }
        }

        public int Number { get; set; }

        public virtual void ExecuteTest()
        {
            Host.Specify(this);
        }

        //[Executable(ExecutionOrder.Initialize, "", Order = -2, ShouldReport = false)]
        //protected virtual void ConfigureContainer()
        //{
        //}

        //[Executable(ExecutionOrder.Initialize, "", Order = -1, ShouldReport = false)]
        //protected virtual void CreateSystemUnderTest()
        //{
        //    //SUT = Container.SystemUnderTest<TSubject>();            
        //}
    }
}