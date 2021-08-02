using UnityEngine;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// A component representing an item.
    /// </summary>
    public abstract class Item : MonoBehaviour
    {
        /// <summary>
        /// The item's data.
        /// </summary>
        [Tooltip("The item's data.")]
        [SerializeField] protected ItemSO _itemData = null;

        /// <summary>
        /// Whether or not the item is currently being held.
        /// </summary>
        [System.NonSerialized] public bool IsBeingHeld = false;

        /// <summary>
        /// Whether or not the item can be picked up.
        /// </summary>
        [System.NonSerialized] public bool CanBePickedUp = true;

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
    }
}
