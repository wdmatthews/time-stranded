using UnityEngine;

namespace Toolkits.ObjectPooling.Test
{
    /// <summary>
    /// Used to test pools.
    /// </summary>
    [CreateAssetMenu(fileName = "TestPool", menuName = "Toolkits/Object Pooling/Test/Pool")]
    public class TestPoolSO : PoolSO<TestComponent>
    {
        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        [Tooltip("The factory to use when creating instances.")]
        [SerializeField] private TestFactorySO _factory = null;

        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        protected override IFactory<TestComponent> Factory => _factory;
    }
}
