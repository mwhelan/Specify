using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestStack.BDDfy;

namespace Specify
{
    /// <summary>
    /// Extension methods for types.
    /// </summary>
    internal static partial class TypeExtensions
    {
        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns>T.</returns>
        internal static T Create<T>(this Type type)
        {
            return (T)type.Create();
        }

        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        internal static object Create(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Determines whether a type is the concrete type of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if type is the concrete type of the specified type; otherwise, <c>false</c>.</returns>
        internal static bool IsConcreteTypeOf<T>(this Type type)
        {
            if (type == null) return false;

            return type.IsConcrete() && type.CanBeCastTo<T>();
        }

        internal static bool IsConcreteTypeOf(this Type type, Type candidateType)
        {
            if (type == null) return false;

            return type.IsConcrete() && type.CanBeCastTo(candidateType);
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
        internal static bool IsIn<T>(this T target, IList<T> list)
        {
            return list.Contains(target);
        }

        /// <summary>
        /// Determines whether the specified type is simple.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is simple; otherwise, <c>false</c>.</returns>
        internal static bool IsSimple(this Type type)
        {
            return type.IsPrimitive() || type == typeof(string) || type.IsEnum();
        }

        internal static readonly List<Type> EnumerableTypes = new List<Type>
        {
            typeof (IEnumerable<>),
            typeof (IList<>),
            typeof (List<>)
        };

        /// <summary>
        /// Returns true if constructor has any value type parameters.
        /// </summary>
        /// <param name="constructor">The type.</param>
        /// <returns>True if constructor has any value type parameters, False if not</returns>
        internal static bool HasAnyValueTypeParameters(this ConstructorInfo constructor)
        {
            return constructor.GetParameters().Any(parameter => parameter.ParameterType.IsValueType());
        }

        internal static bool CanBeResolvedUsingContainer(this Type type, Func<Type, bool> containerCanResolve, bool requiresRegistration = true)
        {
            if (type.IsClass())
            {
                var constructor = type.GreediestConstructor();

                if (constructor == null)
                {
                    return containerCanResolve(type);
                }

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!containerCanResolve(parameterInfo.ParameterType))
                    {
                        if (!parameterInfo.ParameterType.CanBeResolvedUsingContainer(containerCanResolve, requiresRegistration))
                        {
                            return false;
                        }
                    }
                }

                if (!requiresRegistration)
                {
                    return true;
                }
            }

            return containerCanResolve(type);
        }
    }

#if NET46
    internal static partial class TypeExtensions
    {
        /// <summary>
        /// Determines whether this instance [can be cast to] the specified destination type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns><c>true</c> if this instance [can be cast to] the specified destination type; otherwise, <c>false</c>.</returns>
        internal static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type is enumerable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is enumerable; otherwise, <c>false</c>.</returns>
        internal static bool IsEnumerable(this Type type)
        {
            if (type.IsArray) return true;

            return type.IsGenericType()
                && type.GetGenericTypeDefinition().IsIn(EnumerableTypes);
        }

        internal static bool HasOneReferenceTypeGenericArg(this Type type)
        {
            if (!type.IsGenericType()) return false;

            var genericArgs = type.GetGenericArguments();
            return genericArgs.Length == 1
                   && !genericArgs[0].IsSimple();
        }

        public static Type[] GetGenericTypeArguments(this Type type) 
        {
            return type.GetGenericArguments();
        }

        internal static PropertyInfo GetPropertyInfo(this Type type, string propertyName, Type propertyType)
        {
            return type.GetProperty(propertyName, propertyType);
        }

        internal static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName);
        }

        /// <summary>
        /// Returns the constructor with the most parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>ConstructorInfo.</returns>
        internal static ConstructorInfo GreediestConstructor(this Type type)
        {
            return type.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();
        }

        internal static Type BaseType(this Type type)
        {
            return type.BaseType;
        }

        internal static MethodInfo GetMethodInfo(this Type type, string name, Type[] types)
        {
            return type.GetMethod(name, types);
        }

        internal static bool IsClass(this Type type)
        {
            return type.IsClass;
        }

        internal static bool IsSealed(this Type type)
        {
            return type.IsSealed;
        }

        internal static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }

        internal static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }

        internal static bool IsGenericType(this Type type)
        {
            return type.IsGenericType;
        }

        internal static bool IsInterface(this Type type)
        {
            return type.IsInterface;
        }

        internal static bool IsPrimitive(this Type type)
        {
            return type.IsPrimitive;
        }

        internal static bool IsTypeAbstract(this Type type)
        {
            return type.IsAbstract;
        }
    }
#else
    internal static partial class TypeExtensions
    {
        /// <summary>
        /// Determines whether this instance [can be cast to] the specified destination type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="destinationType">Type of the destination.</param>
        /// <returns><c>true</c> if this instance [can be cast to] the specified destination type; otherwise, <c>false</c>.</returns>
        internal static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.GetTypeInfo().IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type is enumerable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is enumerable; otherwise, <c>false</c>.</returns>
        internal static bool IsEnumerable(this Type type)
        {
            if (type.GetTypeInfo().IsArray) return true;

            return type.IsGenericType()
                && type.GetTypeInfo().GetGenericTypeDefinition().IsIn(EnumerableTypes);
        }

        internal static bool HasOneReferenceTypeGenericArg(this Type type)
        {
            if (!type.IsGenericType()) return false;

            var genericArgs = type.GetTypeInfo().GetGenericArguments();
            return genericArgs.Length == 1
                   && !genericArgs[0].IsSimple();
        }

        public static Type[] GetGenericTypeArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments;
        }

        internal static PropertyInfo GetPropertyInfo(this Type type, string propertyName, Type propertyType)
        {
            return type.GetTypeInfo().GetProperty(propertyName, propertyType);
        }

        internal static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            return type.GetTypeInfo().GetProperty(propertyName);
        }

        /// <summary>
        /// Returns the constructor with the most parameters.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>ConstructorInfo.</returns>
        internal static ConstructorInfo GreediestConstructor(this Type type)
        {
            return type.GetTypeInfo().GetConstructors(BindingFlags.Public|BindingFlags.Instance)
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();
        }

        internal static Type BaseType(this Type type)
        {
            return type.GetTypeInfo().BaseType;
        }

        internal static MethodInfo GetMethodInfo(this Type type, string name, Type[] types)
        {
            return type.GetTypeInfo().GetMethod(name, types);
        }

        internal static bool IsClass(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        internal static bool IsSealed(this Type type)
        {
            return type.GetTypeInfo().IsSealed;
        }

        internal static bool IsConcrete(this Type type)
        {
            return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
        }

        internal static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        internal static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

        internal static bool IsInterface(this Type type)
        {
            return type.GetTypeInfo().IsInterface;
        }

        internal static bool IsPrimitive(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive;
        }

        internal static bool IsTypeAbstract(this Type type)
        {
            return type.GetTypeInfo().IsAbstract;
        }
    }
#endif
}