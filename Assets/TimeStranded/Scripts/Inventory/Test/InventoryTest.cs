using UnityEngine;

namespace TimeStranded.Inventory.Test
{
    /// <summary>
    /// Used to test inventories.
    /// </summary>
    [AddComponentMenu("Time Stranded/Inventory/Test/Test")]
    [DisallowMultipleComponent]
    public class InventoryTest : MonoBehaviour
    {
        /// <summary>
        /// The inventory to test.
        /// </summary>
        [Tooltip("The inventory to test.")]
        [SerializeField] private InventorySO _inventory = null;

        /// <summary>
        /// The item to test.
        /// </summary>
        [Tooltip("The item to test.")]
        [SerializeField] private ItemSO _item = null;

        private void Start()
        {
            // Add 2 items.
            _inventory.AddItem(_item, 2);
            Debug.Log("Added 2 items.");

            // Remove 1 item.
            _inventory.RemoveItem(_item, 1);
            Debug.Log("Removed 1 item.");

            // Get the number of items.
            Debug.Log($"There is {_inventory.GetItemCount(_item)} item left.");
        }
    }
}
