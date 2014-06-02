using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Specify.Configuration
{
    public class StartupScanner
    {
        public ISpecifyConfig GetTestLifecycle()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(AssembliesToScanForStartupConfig);
            List<Type> lifecycleTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                var typesInAssembly = GetTypesSafely(assembly)
                    .Where(ClassIsSpecifyConfig())
                    .ToList();
                lifecycleTypes.AddRange(typesInAssembly);
            }
            if (lifecycleTypes.Count == 1)
            {
                return (ISpecifyConfig)Activator.CreateInstance(lifecycleTypes[0]);
            }
            throw new InvalidOperationException("No class was found that implements ITestLifecyle");
        }

        private static Func<Type, bool> ClassIsSpecifyConfig()
        {
            return type => typeof(ISpecifyConfig).IsAssignableFrom(type) && !type.IsInterface;
        }

        Func<Assembly, bool> _assembliesToScanForStartupConfig = assembly => true;
        public Func<Assembly, bool> AssembliesToScanForStartupConfig
        {
            get { return _assembliesToScanForStartupConfig; }
            set { _assembliesToScanForStartupConfig = value; }
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
