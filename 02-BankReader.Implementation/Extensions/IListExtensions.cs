using System;
using System.Collections.Generic;

namespace BankReader.Implementation.Extensions
{
    /// <summary>
    /// Extensions for the IList interface
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Extension method for have the benefit of the AddRange on a IList
        /// </summary>
        /// <param name="list">The list that implements IList</param>
        /// <param name="newItems">The items that should be added to the list</param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> newItems)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            if (newItems == null)
            {
                throw new ArgumentNullException(nameof(newItems));
            }

            if (list is List<T> asList)
            {
                asList.AddRange(newItems);
            }
            else
            {
                foreach (var item in newItems)
                {
                    list.Add(item);
                }
            }
        }
    }
}
