using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Specify.Stories;

namespace Specify
{
    public static class SpecifyExtensions
    {
        public static bool IsScenarioFor(this Type type)
        {
            if (!type.CanBeCastTo<ISpecification>())
                return false;
            return !type.IsSpecificationFor();
        }

        internal static bool IsScenarioFor(this ISpecification specification)
        {
            return specification.Story.Name != "SpecificationStory";
        }

        public static bool IsSpecificationFor(this Type type)
        {
            if (!type.CanBeCastTo<ISpecification>())
                return false;
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
                if (baseType == null) return false;

                type = baseType;
            }
        }

        internal static bool IsSpecificationFor(this ISpecification specification)
        {
            return specification.Story.Name == "SpecificationStory";
        }

        //private static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        //{
        //    var interfaceTypes = givenType.GetInterfaces();

        //    foreach (var type in interfaceTypes)
        //    {
        //        if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
        //            return true;
        //    }

        //    if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
        //        return true;

        //    Type baseType = givenType.BaseType;
        //    if (baseType == null) return false;

        //    return IsAssignableToGenericType(baseType, genericType);
        //}

        private static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            Type destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        private static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }
    }
}
