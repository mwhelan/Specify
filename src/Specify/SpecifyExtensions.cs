using System;
using System.Linq;
using Specify.Stories;

namespace Specify
{
    public static class SpecifyExtensions
    {
        public static bool IsStoryScenario(this Type type)
        {
            if (!type.CanBeCastTo<IScenario>())
                return false;
            return !type.IsUnitScenario();
        }

        internal static bool IsStoryScenario(this IScenario specification)
        {
            return specification.Story.Name != "SpecificationStory";
        }

        public static bool IsUnitScenario(this Type type)
        {
            if (!type.CanBeCastTo<IScenario>())
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

        internal static bool IsUnitScenario(this IScenario specification)
        {
            return specification.Story.Name == "SpecificationStory";
        }

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
