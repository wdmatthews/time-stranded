using System.Collections.Generic;

namespace Toolkits.ObjectPooling
{
    /// <summary>
    /// An interface used to specify how an object pool should work.
    /// </summary>
    /// <typeparam name="T">The type of object to store in the pool.</typeparam>
    public interface IPool<T>
    {
        /// <summary>
        /// Prefills the pool with <paramref name="count"/> number of instances.
        /// </summary>
        /// <param name="count">The number of instances to create.</param>
        void Prefill(int count);

        /// <summary>
        /// Requests an instance from the pool, creating a new one if needed.
        /// </summary>
        /// <returns>An instance of type <typeparamref name="T"/> from the pool.</returns>
        T Request();

        /// <summary>
        /// Requests multiple instances from the pool, creating new ones if needed.
        /// </summary>
        /// <param name="count">The number of instances to request.</param>
        /// <returns>A collection of instances of type <typeparamref name="T"/>.</returns>
        IEnumerable<T> Request(int count);

        /// <summary>
        /// Returns an instance from the pool.
        /// </summary>
        /// <param name="instance">An instance of type <typeparamref name="T"/>.</param>
        void Return(T instance);

        /// <summary>
        /// Returns multiple instances to the pool.
        /// </summary>
        /// <param name="instances">A collection of instances of type <typeparamref name="T"/>.</param>
        void Return(IEnumerable<T> instances);
    }
}
