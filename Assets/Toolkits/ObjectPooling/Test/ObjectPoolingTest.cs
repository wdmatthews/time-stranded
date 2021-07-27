using UnityEngine;

namespace Toolkits.ObjectPooling.Test
{
    /// <summary>
    /// Used to test object pooling.
    /// </summary>
    [AddComponentMenu("Toolkits/Object Pooling/Test/Test")]
    [DisallowMultipleComponent]
    public class ObjectPoolingTest : MonoBehaviour
    {
        /// <summary>
        /// The pool to store test components in.
        /// </summary>
        [Tooltip("The pool to store test components in.")]
        [SerializeField] private TestPoolSO _pool = null;

        /// <summary>
        /// The item count used for the test pool.
        /// </summary>
        [Tooltip("The item count used for the test pool.")]
        [SerializeField] private int _count = 2;

        private void Start()
        {
            // Prefill the pool with multiple instances.
            _pool.Prefill(_count);
            Debug.Log($"Prefilled the pool with {_count} instances.");

            // Request one instance.
            TestComponent instance = _pool.Request();
            instance.OnRequest();
            Debug.Log("Requested one instance from the pool.");

            // Request multiple instances.
            TestComponent[] instances = (TestComponent[])_pool.Request(_count);

            for (int i = instances.Length - 1; i >= 0; i--)
            {
                instances[i].OnRequest();
            }

            Debug.Log($"Requested {_count} instance from the pool.");

            // Return one instance.
            _pool.Return(instance);
            instance.OnReturn();
            Debug.Log("Returned one instance to the pool.");

            // Return multiple instances.
            _pool.Return(instances);

            for (int i = instances.Length - 1; i >= 0; i--)
            {
                instances[i].OnReturn();
            }

            Debug.Log($"Returned {_count} instances to the pool.");
        }
    }
}
