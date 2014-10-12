using System;

namespace Specify
{
    public static class SpecifyExtensions
    {
        public static ISpecification Specify(this object testObject)
        {
            Guard.Against(testObject == null, "testObject cannot be null");
            return Host.PerformTest(testObject.GetType());
        }

        public static bool IsScenarioFor(this ISpecification specification)
        {
            return specification.GetType().IsAssignableToGenericType(typeof(ScenarioFor<,>));
        }

        public static bool IsSpecificationFor(this ISpecification specification)
        {
            return specification.GetType().IsAssignableToGenericType(typeof(SpecificationFor<>));
        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
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
