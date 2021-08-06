using UnityEngine;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// Stores information about an item.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "Time Stranded/Inventory/Item")]
    public class ItemSO : ScriptableObject
    {
        /// <summary>
        /// The pool used to spawn items.
        /// </summary>
        [Tooltip("The pool used to spawn items.")]
        public ItemPoolSO Pool = null;

        /// <summary>
        /// The sprite to use for the item.
        /// </summary>
        [Tooltip("The sprite to use for the item.")]
        public Sprite Sprite = null;

        /// <summary>
        /// Whether or not the item can be selected.
        /// For example, a ball cannot be selected,
        /// but it can replace whatever ability the character selected.
        /// </summary>
        public virtual bool CanBeSelected => false;

        /// <summary>
        /// Whether or not the item can be used up after one use.
        /// </summary>
        public virtual bool IsOneTimeUse => false;

        /// <summary>
        /// Whether or not the item will be released when the character holding it dies.
        /// </summary>
        public virtual bool ReleaseOnCharacterDeath => false;

        /// <summary>
        /// Requests an item of this type from the pool.
        /// </summary>
        /// <returns>An instance of the item.</returns>
        public Item Request()
        {
            Item item = Pool.Request();
            item.Return = Return;
            item.SetData(this);
            item.gameObject.SetActive(true);
            return item;
        }

        /// <summary>
        /// Returns an item to the pool.
        /// </summary>
        /// <param name="item">The item to return.</param>
        public void Return(Item item)
        {
            item.gameObject.SetActive(false);
            Pool.Return(item);
        }
    }
}
