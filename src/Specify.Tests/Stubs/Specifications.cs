namespace Specify.Tests.Stubs
{
    class StubSpecificationFor : SpecificationFor<ConcreteObjectWithMultipleConstructors> { }
    class StubScenarioFor : ScenarioFor<ConcreteObjectWithMultipleConstructors> { }

    class ConcreteObjectWithNoConstructorSpecification : SpecificationFor<ConcreteObjectWithNoConstructor> { }
}
