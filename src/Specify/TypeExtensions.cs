using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static bool CanBeCastTo<T>(this Type type)
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
        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Casts the target to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast the target to.</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>T.</returns>
        internal static T As<T>(this object target) where T : class
        {
            return target as T;
        }

        /// <summary>
        /// Determines whether the specified target is in the list.
        /// </summary>
        /// <typeparam name="T">The type of the item that might be in the list.</typeparam>
        /// <param name="target">The item that might be in the list.</param>
        /// <param name="list">The list of items to check.</param>
        /// <returns><c>true</c> if the specified item is in the list; otherwise, <c>false</c>.</returns>
        public static bool IsIn<T>(this T target, IList<T> list)
        {
            return list.Contains(target);
        }

        /// <summary>
        /// Determines whether the specified type is simple.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is simple; otherwise, <c>false</c>.</returns>
        public static bool IsSimple(this Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type.IsEnum;
        }
        /// <summary>
        /// Determines whether the specified type is enumerable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is enumerable; otherwise, <c>false</c>.</returns>
        public static bool IsEnumerable(this Type type)
        {
            if (type.IsArray) return true;

            return type.IsGenericType 
                && type.GetGenericTypeDefinition().IsIn(EnumerableTypes);
        }

        private static bool HasOneReferenceTypeGenericArg(this Type type)
        {
            if (!type.IsGenericType) return false;

            var genericArgs = type.GetGenericArguments();
            return genericArgs.Length == 1
                   && !genericArgs[0].IsSimple();
        }

        private static readonly List<Type> EnumerableTypes = new List<Type>
        {
            typeof (IEnumerable<>),
            typeof (IList<>),
            typeof (List<>)
        };

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

        /// <summary>
        /// Returns the constructor with the most parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>ConstructorInfo.</returns>
        public static ConstructorInfo GreediestConstructor(this Type type)
        {
            return type.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
        }
    }
}