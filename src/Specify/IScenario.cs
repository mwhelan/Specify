using System;
using TestStack.BDDfy;

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
        ContainerFor<TSut> Container { get; }

        /// <summary>
        /// Gets or sets the SUT (System Under Test). The class being tested.
        /// </summary>
        /// <value>The sut.</value>
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
        Type Story { get; }

        /// <summary>
        /// Gets the title that will be printed on the BDDfy report.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }

        /// <summary>
        /// Optionally specify a number if you want to order scenarios on the report. The Title will then being with 'Scenario [Number]'.
        /// </summary>
        /// <value>The number.</value>
        int Number { get; set; }

        /// <summary>
        /// Used by Specify to set the container for each scenario.
        /// </summary>
        /// <param name="container">The container.</param>
        void SetContainer(IContainer container);
    }
}