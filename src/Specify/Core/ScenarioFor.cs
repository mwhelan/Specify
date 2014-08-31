using System;
using Specify.Containers;

namespace Specify.Core
{
    public abstract class ScenarioFor<TSut, TStory> : Specification<TSut, AutofacSutFactory<TSut>> 
        where TSut : class 
        where TStory : UserStory, new()
    {
        protected ScenarioFor(AutofacSutFactory<TSut> container) : base(container)
        {
        }

        public override Type Story { get { return typeof (TStory); } }
        public abstract int ScenarioNumber { get; }
        public override string Title { get { return string.Format("Scenario {0}: {1}", ScenarioNumber.ToString("00"), Title); } }
    }
}