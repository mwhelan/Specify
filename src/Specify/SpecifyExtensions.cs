using System;

namespace Specify
{
    public static class SpecifyExtensions
    {
        internal static bool IsScenarioFor(this ISpecification specification)
        {
            return specification.GetType().IsAssignableToGenericType(typeof(ScenarioFor<,>));
        }

        internal static bool IsSpecificationFor(this ISpecification specification)
        {
            return specification.GetType().IsAssignableToGenericType(typeof(SpecificationFor<>))
                && !specification.IsScenarioFor();
        }

        internal static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var type in interfaceTypes)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}
