using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracking.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks the enumerable is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">Collection</param>
        /// <returns>TRUE - if collection is null or doesn't contain any element, FALSE - otherwise</returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        /// <summary>
        /// Checks the enumerable is not empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">Collection</param>
        /// <returns>TRUE - if collection is not null and contains any element, FALSE - otherwise</returns>
        public static Boolean IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }
    }
}
