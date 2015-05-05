using System;
using Ninject.Extensions.NamedScope;
using Ninject.Syntax;

namespace Specify.Ninject
{
    /// <summary>
    /// Some utility extensions for Ninject component registration
    /// </summary>
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Registers a component so all dependent components will resolve the same shared instance within the scenario lifetime scope.
        /// </summary>
        public static IBindingNamedWithOrOnSyntax<T> InstancePerScenario<T>(this IBindingInSyntax<T> binding)
        {
            if (binding == null)
                throw new ArgumentNullException("binding");

            return binding.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag);
        }
    }
}
