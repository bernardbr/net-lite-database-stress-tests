using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Domain.Extensions
{
    public static class EnumerableExtension
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing(new Random());
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            var arr = enumerable as T[] ?? enumerable.ToArray();
            var index = rand.Next(0, arr.Count());
            return arr.ElementAt(index);
        }
    }
}
