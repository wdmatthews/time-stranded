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
        protected List<T> _list = new List<T>();

        /// <summary>
        /// How many items are in the list.
        /// </summary>
        public int Count => _list.Count;

        /// <summary>
        /// Returns the item at the given index.
        /// </summary>
        /// <param name="index">The item's index.</param>
        /// <returns>The item.</returns>
        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        /// <summary>
        /// Adds an item to the list.
        /// Override to add event raising.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Add(T item) => _list.Add(item);

        /// <summary>
        /// Adds items from a collection.
        /// </summary>
        /// <param name="collection">The collection of items to add.</param>
        public void AddRange(IEnumerable<T> collection) => _list.AddRange(collection);

        /// <summary>
        /// Removes an item from the list.
        /// Override to add event raising.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public virtual void Remove(T item) => _list.Remove(item);

        /// <summary>
        /// Removes an item from the list at the given index.
        /// Override to add event raising.
        /// </summary>
        /// <param name="index">The item's index.</param>
        public virtual void RemoveAt(int index) => _list.RemoveAt(index);

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear() => _list.Clear();

        /// <summary>
        /// Returns if the item is in the list.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>If the item is found in the list.</returns>
        public bool Contains(T item) => _list.Contains(item);

        /// <summary>
        /// Returns the index of the given item in the list,
        /// or -1 if it is not in the list.
        /// </summary>
        /// <param name="item">The item to search for.</param>
        /// <returns>The item's index.</returns>
        public int IndexOf(T item) => _list.IndexOf(item);
    }
}
