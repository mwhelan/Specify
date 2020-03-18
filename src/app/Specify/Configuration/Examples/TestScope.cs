using System.Collections.Generic;
using System.Linq;
using Specify.Containers;
using Specify.Logging;

namespace Specify.Configuration.Examples
{
    public class TestScope 
    {
        public IChildContainerBuilder Registrations { get; }
        private IEnumerable<IPerScenarioAction> _actions;

        public TestScope(IChildContainerBuilder childContainerBuilder)
        {
            Registrations = childContainerBuilder;
        }

        public virtual void BeginScope<T>(IScenario<T> scenario)
            where T : class
        {
            scenario.RegisterContainerOverrides();
            var childContainer = Registrations.GetChildContainer();
            scenario.Container = new ContainerFor<T>(childContainer);

            _actions = childContainer.GetMultiple<IPerScenarioAction>();
            foreach (var action in _actions.OrderBy(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    this.Log().DebugFormat("Executing {0} PerScenario Before action", action.GetType().Name);
                    action.Before(scenario);
                }
            }
        }

        public virtual void EndScope<T>(IScenario<T> scenario)
            where T : class
        {
            foreach (var action in _actions.OrderByDescending(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    this.Log().DebugFormat("Executing {0} PerScenario After action", action.GetType().Name);
                    action.After();
                }
            }

            scenario.Container?.Dispose();
        }
    }
}
