using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// Provides extensions for <see cref="IList{T}"/>.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// Adapted from Smooth-P's code at
        /// https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            int lastIndex = count - 1;

            for (var i = 0; i < lastIndex; i++)
            {
                int randomIndex = Random.Range(i, count);
                T temporaryValue = list[i];
                list[i] = list[randomIndex];
                list[randomIndex] = temporaryValue;
            }
        }
    }
}
