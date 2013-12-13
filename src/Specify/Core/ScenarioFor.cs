using System;
using Specify.Configuration;
using Specify.Containers;

namespace Specify.Core
{
    public abstract class ScenarioFor<T> : Specification
    {
        public T SUT { get; set; }
        public IDependencyLifetime Container { get; private set; }

        static ScenarioFor()
        {
            SpecifyConfigurator.Initialize();
        }

        public ScenarioFor()
        {
            Container = SpecifyConfigurator.GetDependencyResolver();
            InitialiseSystemUnderTest();
        }

        public virtual void InitialiseSystemUnderTest()
        {
            SUT = Container.Resolve<T>();
        }

        public virtual void TearDown()
        {
            Container.Dispose();
        }

        public abstract override Type Story { get; }
        public abstract int ScenarioNumber { get; }
        public abstract override string Title { get; }

        protected override string BuildTitle()
        {
            return string.Format("Scenario {0}: {1}", ScenarioNumber.ToString("00"), Title);
        }
    }
}