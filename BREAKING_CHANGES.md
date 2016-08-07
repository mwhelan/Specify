# Version 2.0

## Scenarios
Added convenience methods to IScenario interface and ScenarioFor implementation that alias the Container:

    void SetThe<T>()
        where T :  class;
    void SetThe<TService, TImplementation>()
        where TService :  class
        where TImplementation :  class, TService;
    T SetThe<T>(T valueToSet, string key = null)
        where T :  class;
    T The<T>(string key = null)
        where T :  class;
    object The(System.Type serviceType, string key = null);

## Extension Methods

Split some of TypeExtensions out into ScenarioExtensions

	public class static ScenarioExtensions
    {
        public static bool IsScenario(this System.Type type) { }
        public static bool IsStoryScenario(this System.Type type) { }
        public static bool IsStoryScenario(this Specify.IScenario specification) { }
        public static bool IsUnitScenario(this System.Type type) { }
        public static bool IsUnitScenario(this Specify.IScenario specification) { }
    }

Added new StringExtensions class.

	public class static StringExtensions
    {
        public static string ToTitleCase(this string input) { }
    }

## Actions

- Renamed IScenarioActions to `IScenarioAction` and IPerAppDomainActions to `IPerApplicationAction`.
- Changed IScenarioAction `Before` method parameter from IContainer to `IScenario<TSut>`. 

## Bootstrapper
Replaced `IBootstrapSpecify` `GetMockFactory` delegate with simple `MockFactory` property.

## Mock Factories
Added the `MockTypeName` property for the name of the type in the mock provider used for creating mocks.

Added the `IsProviderAvailable` and `MockProviderName` properties to the `IMockFactory` interface.

	bool IsProviderAvailable { get; }
    string MockProviderName { get; }