using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// A readable <see cref="ScriptableObject"/> dictionary.
    /// </summary>
    public abstract class DictionarySO<T> : ScriptableObject where T : ScriptableObject
    {
        /// <summary>
        /// The collection of data.
        /// </summary>
        [Tooltip("The collection of data.")]
        public T[] Collection = { };

        /// <summary>
        /// All of the data by name.
        /// </summary>
        [System.NonSerialized] protected Dictionary<string, T> _dataByName = null;

        /// <summary>
        /// Gets a datum by name.
        /// </summary>
        /// <param name="name">The name of the datum.</param>
        /// <returns>The datum.</returns>
        public T this[string name]
        {
            get
            {
                // If the data were not loaded by name, fill the dictionary.
                if (_dataByName == null)
                {
                    _dataByName = new Dictionary<string, T>();

                    for (int i = Collection.Length - 1; i >= 0; i--)
                    {
                        T datum = Collection[i];
                        _dataByName.Add(datum.name, datum);
                    }
                }

                return _dataByName[name];
            }
        }
    }
}
