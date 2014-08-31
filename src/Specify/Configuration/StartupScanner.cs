using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Specify.Configuration
{
    public class StartupScanner
    {
        public ISpecifyConfig GetConfig(Type type)
        {
            var assembly = type.Assembly;
            List<Type> lifecycleTypes = new List<Type>();
            var typesInAssembly = GetTypesSafely(assembly)
                .Where(ClassIsSpecifyConfig())
                .ToList();
            lifecycleTypes.AddRange(typesInAssembly);

            if (lifecycleTypes.Count == 1)
            {
                return (ISpecifyConfig)Activator.CreateInstance(lifecycleTypes[0]);
            }
            throw new InvalidOperationException("No class was found that implements ISpecifyConfig");
        }

        private static Func<Type, bool> ClassIsSpecifyConfig()
        {
            return type => typeof(ISpecifyConfig).IsAssignableFrom(type) && !type.IsInterface;
        }

        private static IEnumerable<Type> GetTypesSafely(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(x => x != null);
            }
        }
    }

}
