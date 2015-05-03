using System;
using System.Globalization;
using Specify.Stories;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    public abstract class ScenarioFor<TSut>
        : ScenarioFor<TSut, SpecificationStory> where TSut : class { }

    public abstract class ScenarioFor<TSut, TStory> : IScenario
        where TSut : class
        where TStory : Stories.Story, new()
    {
        public SutFactory<TSut> Container { get; internal set; }
        public ExampleTable Examples { get; set; }

        public TSut SUT
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
                if (this.IsUnitScenario())
                {
                    return typeof(TSut).Name;
                }
                var title = Configurator.Scanners.Humanize(GetType().Name);
                title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title);
                if (Number != 0)
                {
                    title = string.Format("Scenario {0}: {1}", Number.ToString("00"), title);
                }
                return title;
            }
        }

        public int Number { get; set; }

        public virtual void SetContainer(IScenarioContainer container)
        {
            Container = new SutFactory<TSut>(container);
        }

        public virtual void Specify()
        {
            Host.Specify(this);
        }

        public void Dispose()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }
    }
}