using System;
using System.Collections.Generic;
using System.Reflection;

namespace Specify.lib
{
    /// <summary>
    /// Class that helps to resolve assembly types. 
    /// </summary>
    public static class AssemblyTypeResolver
    {
        /// <summary>
        /// Gets all types from the AppDomain.
        /// </summary>
        /// <returns>IEnumerable&lt;Type&gt;.</returns>
        public static IEnumerable<Type> GetAllTypesFromAppDomain()
        {
            var assembly = Assembly.GetEntryAssembly();
            return assembly.GetExportedTypes();
        }
    }
}