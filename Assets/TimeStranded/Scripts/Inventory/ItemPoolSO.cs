using UnityEngine;
using Toolkits.ObjectPooling;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// Used to pool items.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItemPool", menuName = "Time Stranded/Inventory/Item Pool")]
    public class ItemPoolSO : PoolSO<Item>
    {
        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        [Tooltip("The factory to use when creating instances.")]
        [SerializeField] private ItemFactorySO _factory = null;

        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        protected override FactorySO<Item> Factory => _factory;
    }
}
