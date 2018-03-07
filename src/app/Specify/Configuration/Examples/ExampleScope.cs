using System.Collections.Generic;
using System.Linq;

namespace Specify.Configuration.Examples
{
    public class ExampleScope : IExampleScope
    {
        private readonly IContainer _applicationContainer;
        private IEnumerable<IPerScenarioAction> _actions;

        public ExampleScope(IContainer applicationContainer)
        {
            _applicationContainer = applicationContainer;
        }

        public void BeginScope<T>(IScenario<T> scenario)
            where T : class
        {
            var childContainer = _applicationContainer.Get<IContainer>();
            scenario.Container = new ContainerFor<T>(childContainer);

            _actions = childContainer.GetMultiple<IPerScenarioAction>();
            foreach (var action in _actions.OrderBy(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    action.Before(scenario);
                }
            }
        }

        public void EndScope<T>(IScenario<T> scenario)
            where T : class
        {
            foreach (var action in _actions.OrderByDescending(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    action.After();
                }
            }

            scenario.Container?.Dispose();
        }
    }
}
