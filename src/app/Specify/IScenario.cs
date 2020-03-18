using System;
using Specify.Configuration;
using Specify.Configuration.Examples;
using Specify.Exceptions;
using TestStack.BDDfy;
using Story = Specify.Stories.Story;

namespace Specify
{
    /// <summary>
    /// A Specify scenario
    /// </summary>
    public interface IScenario<TSut> : IScenario where TSut : class
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        ContainerFor<TSut> Container { get; set; }

        /// <summary>
        /// Gets or sets the SUT (System Under Test). The class being tested.
        /// </summary>
        /// <value>The sut.</value>
        // ReSharper disable once InconsistentNaming
        TSut SUT { get; set; }
    }

    /// <summary>
    /// A Specify scenario
    /// </summary>
    public interface IScenario : IDisposable
    {
        /// <summary>
        /// Executes the scenario using BDDfy.
        /// </summary>
        void Specify();

        /// <summary>
        /// Gets or sets the BDDfy examples to run multiple test cases for this scenario.
        /// </summary>
        /// <value>The examples.</value>
        ExampleTable Examples { get; set; }

        /// <summary>
        /// Gets the story type.
        /// </summary>
        /// <value>The story.</value>
        Story Story { get; }

        /// <summary>
        /// Gets the title that will be printed on the BDDfy report.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }

        /// <summary>
        /// Optionally specify a number if you want to order scenarios on the report. The Title will then being with '[Number]:'.
        /// </summary>
        /// <value>The number.</value>
        int Number { get; }

        /// <summary>
        /// The current test case (example). Increments from 1 to the total count of examples.
        /// </summary>
        /// <value>The number.</value>
        int TestCaseNumber { get; }

        /// <summary>
        /// Used by Specify to set the example scope for each scenario.
        /// </summary>
        /// <param name="testScope">The example scope which manages the Container lifetime for each example.</param>
        void SetExampleScope(ITestScope testScope);

        /// <summary>
        /// Place to register container overrides using TestScope.Registrations methods.
        /// </summary>
        void RegisterContainerOverrides();

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        /// <exception cref="InterfaceResolutionException"></exception>
        T The<T>(string key = null) where T : class;

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        /// <exception cref="InterfaceResolutionException"></exception>
        object The(Type serviceType, string key = null);

        /// <summary>
        /// Registers a type to the container.
        /// </summary>
        /// <typeparam name="T">The type of the component implementation.</typeparam>
        /// <exception cref="InterfaceRegistrationException"></exception>
        void SetThe<T>() where T : class;

        /// <summary>
        /// Registers an implementation type for a service interface
        /// </summary>
        /// <typeparam name="TService">The interface type</typeparam>
        /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        /// <exception cref="InterfaceRegistrationException"></exception>
        void SetThe<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        /// <exception cref="InterfaceRegistrationException"></exception>
        T SetThe<T>(T valueToSet, string key = null) where T : class;
    }
}