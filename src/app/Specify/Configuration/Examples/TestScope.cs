using System;
using System.Collections.Generic;
using System.Linq;
using Specify.Containers;
using Specify.Exceptions;
using Specify.Logging;

namespace Specify.Configuration.Examples
{
    public class TestScope
    {
        private readonly IChildContainerBuilder _childContainerBuilder;
        private IEnumerable<IPerScenarioAction> _actions;

        public TestScope(IChildContainerBuilder childContainerBuilder)
        {
            _childContainerBuilder = childContainerBuilder;
        }

        internal virtual void BeginScope<T>(IScenario<T> scenario)
            where T : class
        {
            scenario.RegisterContainerOverrides();
            var childContainer = _childContainerBuilder.GetChildContainer();
            scenario.Container = new ContainerFor<T>(childContainer);

            _actions = childContainer.GetMultiple<IPerScenarioAction>();
            foreach (var action in _actions.OrderBy(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    this.Log().DebugFormat("Executing {0} PerScenario Before action", action.GetType().Name);
                    action.Before(scenario);
                }
            }
        }

        internal virtual void EndScope<T>(IScenario<T> scenario)
            where T : class
        {
            foreach (var action in _actions.OrderByDescending(x => x.Order))
            {
                if (action.ShouldExecute(scenario.GetType()))
                {
                    this.Log().DebugFormat("Executing {0} PerScenario After action", action.GetType().Name);
                    action.After();
                }
            }

            scenario.Container?.Dispose();
        }

        /// <summary>
        /// Registers a type to the container.
        /// </summary>
        /// <typeparam name="T">The type of the component implementation.</typeparam>
        /// <exception cref="InterfaceRegistrationException"></exception>
        public void Set<T>() where T : class
        {
            _childContainerBuilder.Set<T>();
        }

        /// <summary>
        /// Registers an implementation type for a service interface
        /// </summary>
        /// <typeparam name="TService">The interface type</typeparam>
        /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        /// <exception cref="InterfaceRegistrationException"></exception>
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _childContainerBuilder.Set<TService, TImplementation>();
        }

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        /// <exception cref="InterfaceRegistrationException"></exception>
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            return _childContainerBuilder.Set(valueToSet, key);
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            _childContainerBuilder.SetMultiple(baseType, implementationTypes);
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            _childContainerBuilder.SetMultiple<T>(implementationTypes);
        }
    }
}
