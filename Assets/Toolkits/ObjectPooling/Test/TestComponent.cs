using UnityEngine;

namespace Toolkits.ObjectPooling.Test
{
    /// <summary>
    /// Used to test instance creation in a pool.
    /// </summary>
    [AddComponentMenu("Toolkits/Object Pooling/Test/Component")]
    [DisallowMultipleComponent]
    public class TestComponent : MonoBehaviour
    {
        /// <summary>
        /// Called by <see cref="TestFactorySO"/> when creating an instance.
        /// </summary>
        public void OnCreate()
        {
            Debug.Log("Created TestComponent instance.");
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Called by <see cref="ObjectPoolingTest"/> when requesting an instance.
        /// </summary>
        public void OnRequest()
        {
            Debug.Log("Requested TestComponent instance.");
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Called by <see cref="ObjectPoolingTest"/> when returning an instance.
        /// </summary>
        public void OnReturn()
        {
            Debug.Log("Returned TestComponent instance.");
            gameObject.SetActive(false);
        }
    }
}
