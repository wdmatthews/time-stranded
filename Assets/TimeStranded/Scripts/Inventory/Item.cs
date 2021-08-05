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
        [System.NonSerialized] public ItemSO ItemData = null;

        /// <summary>
        /// Whether or not the item is currently being held.
        /// </summary>
        [System.NonSerialized] public bool IsBeingHeld = false;

        /// <summary>
        /// Whether or not the item can be picked up.
        /// </summary>
        [System.NonSerialized] public bool CanBePickedUp = true;

        /// <summary>
        /// Whether or not the item can deal damage when coming into contact with a character.
        /// </summary>
        [System.NonSerialized] public bool CanDealDamage = false;

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
            ItemData = data;
        }

        /// <summary>
        /// Uses the item on the given character.
        /// </summary>
        /// <param name="character">The character to use the item on.</param>
        /// <returns>If the item was used successfully or not.</returns>
        public abstract bool Use(MonoBehaviour character);

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
        /// Called when the item hits a character.
        /// </summary>
        /// <param name="character">The character that was hit.</param>
        public virtual void OnHit(MonoBehaviour character) { }

        /// <summary>
        /// Returns this item to the pool.
        /// </summary>
        public void ReturnToPool() => Return?.Invoke(this);
    }
}
