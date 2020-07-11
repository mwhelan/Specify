using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTemplate.Api.Domain.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            var items = enumerable as T[] ?? enumerable.ToArray();
            foreach (var item in items)
            {
                action(item);
            }

            return items;
        }
    }
}