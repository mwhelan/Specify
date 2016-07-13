using System;
using System.Linq;
using Specify.Stories;

namespace Specify
{
    /// <summary>
    /// Extension methods for types.
    /// </summary>
    public static class ScenarioExtensions
    {
        /// <summary>
        /// Determines whether the specified type is scenario.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is scenario; otherwise, <c>false</c>.</returns>
        public static bool IsScenario(this Type type)
        {
            return type.CanBeCastTo<IScenario>() && !type.IsTypeAbstract();
        }

        /// <summary>
        /// Determines whether the specified type is a scenario with a Story that is not SpecificationStory (for unit tests).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is story scenario] [the specified type]; otherwise, <c>false</c>.</returns>
        public static bool IsStoryScenario(this Type type)
        {
            if (!type.CanBeCastTo<IScenario>())
                return false;
            return !GenericTypeIsSpecificationStory(type);
        }

        /// <summary>
        /// Determines whether the specified type is a scenario with a Story that is not SpecificationStory (for unit tests).
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns><c>true</c> if [is story scenario] [the specified specification]; otherwise, <c>false</c>.</returns>
        public static bool IsStoryScenario(this IScenario specification)
        {
            return specification.Story != typeof(SpecificationStory);
        }

        /// <summary>
        /// Determines whether the specified type is a scenario for unit tests).
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if [is unit scenario] [the specified type]; otherwise, <c>false</c>.</returns>
        public static bool IsUnitScenario(this Type type)
        {
            if (!type.CanBeCastTo<IScenario>())
                return false;
            return GenericTypeIsSpecificationStory(type);
        }

        /// <summary>
        /// Determines whether the specified type is a scenario for unit tests).
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns><c>true</c> if [is unit scenario] [the specified specification]; otherwise, <c>false</c>.</returns>
        public static bool IsUnitScenario(this IScenario specification)
        {
            return specification.Story == typeof(SpecificationStory);
        }

        internal static Type SutType(this IScenario specification)
        {
            return specification.GetType().GetPropertyInfo("SUT").PropertyType;
        }

        /// <summary>
        /// Whether or not a generic argument is of type SpecificationStory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool GenericTypeIsSpecificationStory(Type type)
        {
            while (true)
            {
                var genericArguments = type.GetGenericTypeArguments();
                if (genericArguments.Length > 0)
                {
                    if (genericArguments.Any(argument => argument == typeof(SpecificationStory)))
                    {
                        return true;
                    }
                }

                Type baseType = type.BaseType();
                if (baseType == null)
                {
                    return false;
                }

                type = baseType;
            }
        }

    }
}