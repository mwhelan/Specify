using System;
using Specify.Stories;

namespace Specify
{
    public static class SpecifyExtensions
    {
        public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            Type destinationType = typeof(T);

            return CanBeCastTo(type, destinationType);
        }

        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }

        public static bool IsScenarioFor(this Type type)
        {
            //return specification.IsAssignableToGenericType(typeof(ScenarioFor<,>));
            if (!type.CanBeCastTo<ISpecification>())
                return false;
            var specification = type as ISpecification;
            return specification.Story.CanBeCastTo<UserStory>() || specification.Story.CanBeCastTo<ValueStory>();
        }

        internal static bool IsScenarioFor(this ISpecification specification)
        {
            return specification.GetType().IsScenarioFor();
        }

        public static bool IsSpecificationFor(this Type specification)
        {
            return specification.IsAssignableToGenericType(typeof(SpecificationFor<>))
                && !specification.IsScenarioFor();
        }

        internal static bool IsSpecificationFor(this ISpecification specification)
        {
            return specification.GetType().IsSpecificationFor()
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
