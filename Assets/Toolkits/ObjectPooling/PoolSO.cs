using System.Collections.Generic;
using UnityEngine;

namespace Toolkits.ObjectPooling
{
    public abstract class PoolSO<T> : ScriptableObject
    {
        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        protected abstract FactorySO<T> Factory { get; }

        /// <summary>
        /// The list of items in the pool.
        /// </summary>
        protected readonly Stack<T> _items = new Stack<T>();

        /// <summary>
        /// Whether or not the pool was already prefilled.
        /// </summary>
        [System.NonSerialized] protected bool _wasPrefilled = false;

        /// <summary>
        /// Prefills the pool with <paramref name="count"/> number of instances.
        /// </summary>
        /// <param name="count">The number of instances to create.</param>
        public void Prefill(int count)
        {
            if (_wasPrefilled) return;

            for (int i = 0; i < count; i++)
            {
                _items.Push(Factory.Create());
            }

            _wasPrefilled = true;
        }

        /// <summary>
        /// Requests an instance from the pool, creating a new one if needed.
        /// </summary>
        /// <returns>An instance of type T from the pool.</returns>
        public T Request()
        {
            return _items.Count > 0 ? _items.Pop() : Factory.Create();
        }

        /// <summary>
        /// Requests multiple instances from the pool, creating new ones if needed.
        /// </summary>
        /// <param name="count">The number of instances to request.</param>
        /// <returns>A collection of instances of type T.</returns>
        public IEnumerable<T> Request(int count)
        {
            T[] instances = new T[count];

            for (int i = 0; i < count; i++)
            {
                instances[i] = Request();
            }

            return instances;
        }

        /// <summary>
        /// Returns an instance to the pool.
        /// </summary>
        /// <param name="instance">An instance of type T.</param>
        public void Return(T instance)
        {
            _items.Push(instance);
        }

        /// <summary>
        /// Returns multiple instances to the pool.
        /// </summary>
        /// <param name="instances">A collection of instances of type T.</param>
        public void Return(IEnumerable<T> instances)
        {
            foreach (T instance in instances)
            {
                _items.Push(instance);
            }
        }
    }
}
