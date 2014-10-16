using System;
using Specify.Containers;

namespace Specify
{
    internal class TestRunner : IDisposable
    {
        private readonly IDependencyResolver _container;
        private readonly ITestEngine _testEngine;

        public TestRunner(IDependencyResolver container, ITestEngine testEngine)
        {
            _container = container;
            _testEngine = testEngine;
        }

        public ISpecification Run(ISpecification testObject)
        {
            if (testObject == null)
            {
                throw new ArgumentNullException("testObject");
            }

            using (var lifetimeScope = _container.CreateScope())
            {
                var specification = (ISpecification)lifetimeScope.Resolve(testObject.GetType());
                _testEngine.Execute(specification, specification.Title);
                return specification;
            }
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public IDependencyResolver Container
        {
            get { return _container; }
        }

        public ITestEngine TestEngine
        {
            get { return _testEngine; }
        }
    }
}