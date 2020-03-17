using System;
using System.Collections.Generic;
using Specify.Containers;
using TinyIoC;

namespace Specify
{
    public class TinyChildContainerBuilder : IChildContainerBuilder
    {
        private readonly IContainerRoot _rootContainer;

        public List<Action<TinyIoCContainer>> BuilderActions { get; } = new List<Action<TinyIoCContainer>>();

        public TinyChildContainerBuilder(IContainerRoot rootContainer)
        {
            if (!(rootContainer is TinyContainer))
            {
                throw new ArgumentException();
            }

            _rootContainer = rootContainer;
        }

        public IContainer GetChildContainer()
        {
            var childContainer = _rootContainer.GetChildContainer();

            foreach (var action in BuilderActions)
            {
                action((childContainer as TinyContainer).Container);
            }

            return childContainer;
        }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            BuilderActions.Add(container => container.Register<T>().AsSingleton());
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            BuilderActions.Add(container => container.Register<TService, TImplementation>());
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                BuilderActions.Add(container => container.Register<T>(valueToSet));
            }
            else
            {
                BuilderActions.Add(container => container.Register<T>(valueToSet, key));
            }
            return valueToSet;
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            BuilderActions.Add(container => container.RegisterMultiple(baseType, implementationTypes));
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            SetMultiple(typeof(T), implementationTypes);
        }
    }
}
