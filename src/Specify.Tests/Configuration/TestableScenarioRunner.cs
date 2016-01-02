using System;
using NSubstitute;
using Specify.Configuration;

namespace Specify.Tests.Configuration
{
    internal class TestableScenarioRunner : ScenarioRunner
    {
        private readonly IScenario _specification;

        public  TestableScenarioRunner(IScenario specification)
            : base(new StubConfig(), Substitute.For<ITestEngine>())
        {
            _specification = specification;
            ChildContainer = Substitute.For<IContainer>();
            this.Configuration.ApplicationContainer
                .Resolve<IContainer>()
                .Returns(ChildContainer);
            ChildContainer
                .Resolve(Arg.Any<Type>())
                .Returns(_specification);
        }

        public IContainer ChildContainer;
    }
}