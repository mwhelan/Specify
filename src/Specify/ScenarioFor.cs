using System;
using Specify.Containers;

namespace Specify
{
    public abstract class ScenarioFor<TSut, TStory> : Specification<TSut, IocContainer<TSut>>
        where TSut : class
        where TStory : UserStory
    {
        public override Type Story { get { return typeof(TStory); } }
        public int ScenarioNumber { get; set; }

        public override string Title
        {
            get
            {
                var title = GetType().Name.Humanize(LetterCasing.Title);
                if (ScenarioNumber != 0)
                {
                    title = string.Format("Scenario {0}: {1}", ScenarioNumber.ToString("00"), title);
                }
                return title;
            }
        }
    }

    public abstract class ScenarioFor<TSut> : ScenarioFor<TSut, NullStory>
        where TSut : class
    {
        
    }
}