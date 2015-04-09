using System;
using Ninject;

namespace Specify.Examples.Ninject
{
    static class KernelExtensions
    {
        public static T GetDefault<T>(this IKernel kernel)
        {
            return kernel.Get<T>(m => m.Name == null);
        }

        public static object GetDefault(this IKernel kernel, Type type)
        {
            return kernel.Get(type, m => m.Name == null);
        }

        public static T GetNamedOrDefault<T>(this IKernel kernel, string name)
        {
            T namedResult = kernel.TryGet<T>(name);
            if (namedResult != null)
                return namedResult;
            return kernel.GetDefault<T>();
        }
        public static object GetNamedOrDefault(this IKernel kernel, Type type, string name)
        {
            object namedResult = kernel.TryGet(type, name);
            if (namedResult != null)
                return namedResult;
            return kernel.GetDefault(type);
        }

    }
}