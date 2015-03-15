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
        public SutFactory<TSubject> Context { get; internal set; }
        public ExampleTable Examples { get; set; }

        public TSubject SUT
        {
            get { return Context.SystemUnderTest; }
            set { Context.SystemUnderTest = value; }
        }

        public T Get<T>(string key = null) where T : class
        {
            return Context.Get<T>(key);
        }

        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            return Context.RegisterInstance(valueToSet, key);
        }

        public void Set<T>() where T : class
        {
            Context.RegisterType<T>();
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

        public void SetContainer(IContainer container)
        {
            Context = new SutFactory<TSubject>(container);
        }

        public virtual void Specify()
        {
            Host.Specify(this);
        }
    }
}