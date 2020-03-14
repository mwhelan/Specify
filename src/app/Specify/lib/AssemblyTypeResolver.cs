using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace Specify.lib
{
    /// <summary>
    /// Returns assemblies referencing Specify and types in those assemblies
    /// </summary>
    public static class AssemblyTypeResolver
    {
        /// <summary>
        /// Gets all types from the AppDomain.
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> GetAllTypesFromAppDomain()
        {
            var assemblies = GetAllAssembliesFromAppDomain();
            var types = assemblies.SelectMany(assembly => assembly.ExportedTypes);
            return types;
        }

        /// <summary>
        /// Gets all assemblies from the AppDomain.
        /// </summary>
        /// <returns>IEnumerable&lt;Assembly&gt;.</returns>
        public static IEnumerable<Assembly> GetAllAssembliesFromAppDomain()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            var assembly = Assembly.Load(new AssemblyName("Specify"));
            assemblies.Add(assembly);
            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library))
                {
                    assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Specify"));
        }
    }
}