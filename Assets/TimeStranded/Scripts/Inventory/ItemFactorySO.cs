using UnityEngine;
using Toolkits.ObjectPooling;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// Used to create items.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItemFactory", menuName = "Time Stranded/Inventory/Item Factory")]
    public class ItemFactorySO : FactorySO<Item>
    {
        /// <summary>
        /// The prefab to instantiate when creating an instance.
        /// </summary>
        [Tooltip("The prefab to instantiate when creating an instance.")]
        [SerializeField] private Item _prefab = null;

        /// <summary>
        /// Creates and returns a new instance of an item.
        /// </summary>
        /// <returns>The new instance of the item.</returns>
        public override Item Create()
        {
            Item item = Instantiate(_prefab);
            return item;
        }
    }
}
