using System;
using System.Collections.Generic;

namespace Specify.Containers
{
    public interface IChildContainerBuilder
    {
        IContainer GetChildContainer();

        /// <inheritdoc />
        void Set<T>() where T : class;

        /// <inheritdoc />
        void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <inheritdoc />
        T Set<T>(T valueToSet, string key = null) where T : class;

        /// <inheritdoc />
        void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes);

        void SetMultiple<T>(IEnumerable<Type> implementationTypes);
    }
}