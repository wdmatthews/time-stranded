using UnityEngine;

namespace TimeStranded.Inventory.Test
{
    /// <summary>
    /// Used to test item pools.
    /// </summary>
    [AddComponentMenu("Time Stranded/Inventory/Test/Item Pool Test")]
    [DisallowMultipleComponent]
    public class ItemPoolTest : MonoBehaviour
    {
        /// <summary>
        /// The item to test.
        /// </summary>
        [Tooltip("The item to test.")]
        [SerializeField] private ItemSO _item = null;

        private void Start()
        {
            // Request an instance from the pool.
            Item item = _item.Request();
            Debug.Log("Item requested.", item);

            // Return the instance to the pool.
            item.ReturnToPool();
            Debug.Log("Item returned.", item);
        }
    }
}
