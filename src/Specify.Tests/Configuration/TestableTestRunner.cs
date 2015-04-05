using System;
using NSubstitute;
using Specify.Configuration;
using Specify.Containers;

namespace Specify.Tests.Configuration
{
    internal class TestableTestRunner : TestRunner
    {
        private readonly IScenario _specification;

        public  TestableTestRunner(IScenario specification)
            : base(new StubConfig(), Substitute.For<IDependencyResolver>(),
            Substitute.For<ITestEngine>())
        {
            _specification = specification;
            ChildContainer = Substitute.For<IContainer>();
            DependencyResolver
                .CreateChildContainer()
                .Returns(ChildContainer);
            ChildContainer
                .Resolve(Arg.Any<Type>())
                .Returns(_specification);
        }

        public IContainer ChildContainer;
    }
}