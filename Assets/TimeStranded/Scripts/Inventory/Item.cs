using UnityEngine;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// A component representing an item.
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        /// <summary>
        /// Stores the item's data.
        /// </summary>
        protected ItemSO _itemData = null;

        /// <summary>
        /// Whether or not the item is currently being held.
        /// </summary>
        [System.NonSerialized] public bool IsBeingHeld = false;

        /// <summary>
        /// Whether or not the item can be picked up.
        /// </summary>
        [System.NonSerialized] public bool CanBePickedUp = true;

        /// <summary>
        /// The function called that will return this item to its pool.
        /// </summary>
        public System.Action<Item> Return = null;

        /// <summary>
        /// Sets the item's data.
        /// </summary>
        /// <param name="data">The item's data.</param>
        public virtual void SetData(ItemSO data)
        {
            _itemData = data;
        }

        /// <summary>
        /// Uses the item on the given character.
        /// </summary>
        /// <param name="character">The character to use the item on.</param>
        public abstract void Use(MonoBehaviour character);

        /// <summary>
        /// Called when the item is picked up.
        /// </summary>
        /// <param name="character">The character that picked up the item.</param>
        public virtual void OnPickup(MonoBehaviour character)
        {
            IsBeingHeld = true;
        }

        /// <summary>
        /// Called when the item is released.
        /// </summary>
        /// <param name="character">The character that released the item.</param>
        public virtual void OnRelease(MonoBehaviour character)
        {
            IsBeingHeld = false;
        }

        /// <summary>
        /// Returns this item to the pool.
        /// </summary>
        public void ReturnToPool() => Return?.Invoke(this);
    }
}
