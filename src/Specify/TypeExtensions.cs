using System;
using System.Linq;
using Specify.Stories;

namespace Specify
{
    /// <summary>
    /// Extension methods for types.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type is scenario.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is scenario; otherwise, <c>false</c>.</returns>
        public static bool IsScenario(this Type type)
        {
            return type.CanBeCastTo<IScenario>();
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
        internal static bool IsStoryScenario(this IScenario specification)
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
        internal static bool IsUnitScenario(this IScenario specification)
        {
            return specification.Story == typeof(SpecificationStory);
        }

        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns>T.</returns>
        public static T Create<T>(this Type type)
        {
            return (T)type.Create();
        }

        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public static object Create(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Determines whether a type is the concrete type of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if type is the concrete type of the specified type; otherwise, <c>false</c>.</returns>
        public static bool IsConcreteTypeOf<T>(this Type type)
        {
            if (type == null) return false;

            return type.IsConcrete() && typeof(T).IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type is concrete.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is concrete; otherwise, <c>false</c>.</returns>
        public static bool IsConcrete(this Type type)
        {
            if (type == null) return false;

            return !type.IsAbstract && !type.IsInterface;
        }

        /// <summary>
        /// Determines whether this instance [can be cast to] the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance [can be cast to] the specified type; otherwise, <c>false</c>.</returns>
        internal static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            Type destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        /// <summary>
        /// Determines whether this instance [can be cast to] the specified destination type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns><c>true</c> if this instance [can be cast to] the specified destination type; otherwise, <c>false</c>.</returns>
        private static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
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
                var genericArguments = type.GetGenericArguments();
                if (genericArguments.Length > 0)
                {
                    if (genericArguments.Any(argument => argument == typeof(SpecificationStory)))
                    {
                        return true;
                    }
                }

                Type baseType = type.BaseType;
                if (baseType == null)
                {
                    return false;
                }

                type = baseType;
            }
        }
    }
}
