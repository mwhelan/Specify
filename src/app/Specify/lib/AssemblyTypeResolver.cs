#if NET40
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Specify.lib
{
    /// <summary>
    /// Class that helps to resolve assembly types. If an error occurs while loading assembly types, it will catch and continue
    /// This class was adapted from the DefaultReflector from FluentAssertions by Dennis Doomen.
    /// https://github.com/dennisdoomen/fluentassertions/blob/develop/FluentAssertions.Net40/Common/DefaultReflectionProvider.cs
    /// </summary>
    public static class AssemblyTypeResolver
    {
        /// <summary>
        /// Gets all types from the AppDomain.
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> GetAllTypesFromAppDomain()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !IsDynamic(a))
                .SelectMany(GetExportedTypes).ToArray();
        }

        /// <summary>
        /// Gets all assemblies from the AppDomain.
        /// </summary>
        /// <returns>IEnumerable&lt;Assembly&gt;.</returns>
        public static IEnumerable<Assembly> GetAllAssembliesFromAppDomain()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !IsDynamic(a));
        }

        private static bool IsDynamic(Assembly assembly)
        {
            return (assembly is AssemblyBuilder) ||
                   (assembly.GetType().FullName == "System.Reflection.Emit.InternalAssemblyBuilder");
        }

        private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types;
            }
            catch (FileLoadException)
            {
                return new Type[0];
            }
            catch (Exception)
            {
                return new Type[0];
            }
        }
    }
}
#else
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
            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
        {
            return compilationLibrary.Name == ("Specify")
                || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Specify"));
        }
    }
}
#endif