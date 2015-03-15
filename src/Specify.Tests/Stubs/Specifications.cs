using Specify.Stories;

namespace Specify.Tests.Stubs
{
    class StubSpecificationFor : SpecificationFor<ConcreteObjectWithMultipleConstructors> { }
    class StubScenarioFor : SpecificationFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }

    class ConcreteObjectWithNoConstructorSpecification : SpecificationFor<ConcreteObjectWithNoConstructor> { }
}
