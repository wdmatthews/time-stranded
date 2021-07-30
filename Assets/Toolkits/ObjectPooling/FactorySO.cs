using UnityEngine;

namespace Toolkits.ObjectPooling
{
    /// <summary>
    /// A base factory to create an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    public abstract class FactorySO<T> : ScriptableObject
    {
        /// <summary>
        /// Creates and returns a new instance of an object with type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>The new instance of type <typeparamref name="T"/>.</returns>
        public abstract T Create();
    }
}
