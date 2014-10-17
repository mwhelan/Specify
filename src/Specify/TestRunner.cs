using System;
using Specify.Containers;

namespace Specify
{
    public class TestRunner : IDisposable
    {
        private readonly ITestContainer _container;
        private readonly ITestEngine _testEngine;

        public TestRunner(ITestContainer container, ITestEngine testEngine)
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

            using (var lifetimeScope = _container.CreateTestLifetimeScope())
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

        public ITestContainer Container
        {
            get { return _container; }
        }

        public ITestEngine TestEngine
        {
            get { return _testEngine; }
        }
    }
}