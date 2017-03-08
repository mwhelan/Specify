using System;
using Specify.Stories;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify
{
    /// <summary>
    /// The base class for scenarios without a story (normally unit test scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    public abstract class ScenarioFor<TSut>
        : ScenarioFor<TSut, SpecificationStory> where TSut : class { }

    /// <summary>
    /// The base class for scenarios with a story (normally larger scenarios).
    /// </summary>
    /// <typeparam name="TSut">The type of the SUT.</typeparam>
    /// <typeparam name="TStory">The type of the t story.</typeparam>
    public abstract class ScenarioFor<TSut, TStory> : IScenario<TSut>
        where TSut : class
        where TStory : Stories.Story, new()
    {
        /// <inheritdoc />
        public ContainerFor<TSut> Container { get; internal set; }

        /// <inheritdoc />
        public ExampleTable Examples { get; set; }

        /// <inheritdoc />
        public TSut SUT
        {
            get { return Container.SystemUnderTest; }
            set { Container.SystemUnderTest = value; }
        }

        /// <inheritdoc />
        public virtual Type Story => typeof(TStory);

        /// <inheritdoc />
        public virtual string Title
        {
            get
            {
                var title = Configurator.Scanners
                    .Humanize(GetType().FullName.Replace(GetType().Namespace + ".", string.Empty))
                    .ToTitleCase();
                if (Number != 0)
                {
                    title = $"Scenario {Number:00}: {title}";
                }
                return title;
            }
        }

        /// <inheritdoc />
        public int Number { get; set; }

        /// <inheritdoc />
        public virtual void SetContainer(IContainer container)
        {
            Container = new ContainerFor<TSut>(container);
        }

        /// <inheritdoc />
        public virtual void Specify()
        {
            Host.Specify(this);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Container?.Dispose();
        }

        /// <inheritdoc />
        public T The<T>(string key = null) where T : class 
        {
            return Container.Get<T>(key);
        }

        /// <inheritdoc />
        public object The(Type serviceType, string key = null)
        {
            return Container.Get(serviceType, key);
        }

        /// <inheritdoc />
        public void SetThe<T>() where T : class
        {
            Container.Set<T>();
        }

        /// <inheritdoc />
        public void SetThe<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Set<TService, TImplementation>();
        }

        /// <inheritdoc />
        public T SetThe<T>(T valueToSet, string key = null) where T : class
        {
            return Container.Set<T>(valueToSet, key);
        }
    }
}