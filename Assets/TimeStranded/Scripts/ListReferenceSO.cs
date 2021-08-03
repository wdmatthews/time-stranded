using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// Stores a list of data that can be referenced in the inspector,
    /// such as a list of all characters in a game match.
    /// </summary>
    /// <typeparam name="T">The type of data to store in the list.</typeparam>
    public abstract class ListReferenceSO<T> : ScriptableObject
    {
        /// <summary>
        /// The list being referenced.
        /// </summary>
        [Tooltip("The list being referenced.")]
        public List<T> List = new List<T>();
    }
}
