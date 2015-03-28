using System;
using System.Globalization;
using Specify.Containers;
using Specify.Stories;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    public abstract class SpecificationFor<TSubject> 
        : SpecificationFor<TSubject, SpecificationStory> where TSubject : class { }

    public abstract class SpecificationFor<TSubject, TStory> : ISpecification
        where TSubject : class
        where TStory : Stories.Story, new()
    {
        public SpecificationContext<TSubject> Container { get; internal set; }
        public ExampleTable Examples { get; set; }

        public TSubject SUT
        {
            get { return Container.SystemUnderTest; }
            set { Container.SystemUnderTest = value; }
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
                    var title = Configurator.Scanners.Humanize(GetType().Name);
                    title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                    if (Number != 0)
                    {
                        title = string.Format("Scenario {0}: {1}", Number.ToString("00"), title);
                    }
                    return title;
                }
            }
        }

        public int Number { get; set; }

        public virtual void SetContainer(IContainer container)
        {
            Container = new SpecificationContext<TSubject>(container);
        }

        public virtual void Specify()
        {
            Host.Specify(this);
        }
    }
}