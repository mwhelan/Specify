#pragma warning disable 1591, 1574, 1711, 1712
using System;
using System.Collections.Generic;
using System.Linq;


namespace Specify
{
    public static class EnumerableTypes
    {
        private static readonly List<Type> _enumerableTypes = new List<Type>
        {
            typeof (IEnumerable<>),
            typeof (IList<>),
            typeof (List<>)
        };
        public static Type GetTypeFromEnumerable(this Type enumerableType)
        {
            if (enumerableType.IsArray)
            {
                return typeof(ArrayCoercion<>).MakeGenericType(enumerableType.GetElementType())
                    .Create<IEnumerableCoercion>()
                    .ElementType;
            }

            if (enumerableType.IsGenericType)
            {
                var templateType = enumerableType.GetGenericTypeDefinition();
                if (_enumerableTypes.Contains(templateType))
                {
                    return typeof (ListCoercion<>).MakeGenericType(enumerableType.GetGenericArguments().First())
                        .Create<IEnumerableCoercion>()
                        .ElementType;
                }
            }

            throw new ArgumentException(
                $"Only IEnumerable<T> types can be passed to the GetMultiple method.  {enumerableType.AssemblyQualifiedName} is invalid");
        }
    }
    public interface IEnumerableCoercion
    {
        Type ElementType { get; }
        object Convert(IEnumerable<object> enumerable);
    }
    public class ArrayCoercion<T> : IEnumerableCoercion where T : class
    {
        public object Convert(IEnumerable<object> enumerable)
        {
            return enumerable.Select(x => x as T).ToArray();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }
    }
    public class ListCoercion<T> : IEnumerableCoercion where T : class
    {
        public object Convert(IEnumerable<object> enumerable)
        {
            return enumerable.Select(x => x as T).ToList();
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }
    }
}