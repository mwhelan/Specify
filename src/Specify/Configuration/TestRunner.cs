using System.Linq;
using Specify.Containers;

namespace Specify.Configuration
{
    internal class TestRunner
    {
        private readonly SpecifyConfiguration _configuration;
        private readonly IContainer _container;
        private readonly ITestEngine _testEngine;

        public TestRunner(SpecifyConfiguration configuration, IContainer container,
            ITestEngine testEngine)
        {
            _configuration = configuration;
            _container = container;
            _testEngine = testEngine;
        }

        public void Execute(ISpecification testObject, string scenarioTitle = null)
        {
            foreach (var action in _configuration.PerTestActions)
            {
                action.Before();
            }

            using (var lifetimeScope = _container.CreateChildContainer())
            {
                var specification = (ISpecification)_container.Get(testObject.GetType());
                specification.SetContainer(lifetimeScope);
                _testEngine.Execute(specification);
            }

            foreach (var action in _configuration.PerTestActions.AsEnumerable().Reverse())
            {
                action.After();
            }
        }

        internal IContainer Container { get { return _container; } }
        internal ITestEngine TestEngine { get { return _testEngine; } }
    }
}