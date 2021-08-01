using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded.Inventory
{
    /// <summary>
    /// Stores a list of item stacks for a character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewInventory", menuName = "Time Stranded/Inventory/Inventory")]
    public class InventorySO : ScriptableObject
    {
        /// <summary>
        /// A list of all the item stacks.
        /// </summary>
        [System.NonSerialized] public List<ItemStack> Stacks = new List<ItemStack>();

        /// <summary>
        /// Gets an item's index, or -1 if the inventory does not have it.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The item's index.</returns>
        private int GetStackIndex(ItemSO item)
        {
            for (int i = Stacks.Count - 1; i >= 0; i--)
            {
                if (Stacks[i].Item == item) return i;
            }

            return -1;
        }

        /// <summary>
        /// Adds one or multiple of an item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="amount">How many of the item to add.</param>
        public void AddItem(ItemSO item, int amount)
        {
            // Check if an ItemStack already exists for the item.
            int stackIndex = GetStackIndex(item);
            if (stackIndex >= 0) Stacks[stackIndex].Amount += amount;
            // If not, add it.
            else Stacks.Add(new ItemStack(item, amount));
        }

        /// <summary>
        /// Removes one or multiple of an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="amount">How many of the item to remove.</param>
        public void RemoveItem(ItemSO item, int amount)
        {
            int stackIndex = GetStackIndex(item);
            if (stackIndex < 0) return;
            ItemStack stack = Stacks[stackIndex];
            // Remove from the stack.
            stack.Amount -= amount;
            // Remove empty stacks.
            if (stack.Amount <= 0) Stacks.RemoveAt(stackIndex);
        }

        /// <summary>
        /// Returns how many of an item the inventory has.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>How many of the item the inventory has.</returns>
        public int GetItemCount(ItemSO item)
        {
            int stackIndex = GetStackIndex(item);
            if (stackIndex < 0) return 0;
            return Stacks[stackIndex].Amount;
        }
    }
}
