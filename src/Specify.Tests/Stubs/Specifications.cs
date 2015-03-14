using Specify.Stories;

namespace Specify.Tests.Stubs
{
    class StubSpecificationFor : SpecificationFor<ConcreteObjectWithMultipleConstructors> { }
    class StubScenarioFor : SpecificationFor<ConcreteObjectWithMultipleConstructors, UserStory> { }

    class ConcreteObjectWithNoConstructorSpecification : SpecificationFor<ConcreteObjectWithNoConstructor> { }
}
