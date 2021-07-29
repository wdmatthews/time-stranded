using UnityEngine;

namespace Toolkits.Loading.Test
{
    /// <summary>
    /// Used to test the loading system with a counter.
    /// </summary>
    [AddComponentMenu("Toolkits/Loading/Test/Counter Loading Test")]
    [DisallowMultipleComponent]
    public class CounterLoadingTest : MonoBehaviour
    {
        /// <summary>
        /// The load manager that counts.
        /// </summary>
        [Tooltip("The load manager that counts.")]
        [SerializeField] private CounterLoadManagerSO _counterLoadManager = null;

        private void Start()
        {
            // Start counting from 1 to 100.
            _counterLoadManager.Count(1, 100);
        }
    }
}
