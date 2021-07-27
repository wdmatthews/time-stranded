using UnityEngine;

namespace Toolkits.ObjectPooling.Test
{
    /// <summary>
    /// Used to test instance creation.
    /// </summary>
    [CreateAssetMenu(fileName = "TestFactory", menuName = "Toolkits/Object Pooling/Test/Factory")]
    public class TestFactorySO : FactorySO<TestComponent>
    {
        /// <summary>
        /// The prefab to instantiate when creating an instance.
        /// </summary>
        [Tooltip("The prefab to instantiate when creating an instance.")]
        [SerializeField] private TestComponent _prefab = null;

        /// <summary>
        /// Creates and returns a new instance of a test component.
        /// </summary>
        /// <returns>The new instance of the test component.</returns>
        public override TestComponent Create()
        {
            TestComponent testComponent = Instantiate(_prefab);
            testComponent.OnCreate();
            return testComponent;
        }
    }
}
