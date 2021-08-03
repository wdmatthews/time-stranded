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
        /// <returns>The added item's stack.</returns>
        public ItemStack AddItem(ItemSO item, int amount)
        {
            int stackIndex = GetStackIndex(item);
            ItemStack stack = null;

            // Check if an ItemStack already exists for the item.
            if (stackIndex >= 0)
            {
                stack = Stacks[stackIndex];
                stack.Amount += amount;
            }
            // If not, add it.
            else
            {
                stack = new ItemStack(item, amount);
                Stacks.Add(stack);
            }

            return stack;
        }

        /// <summary>
        /// Removes one or multiple of an item.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="amount">How many of the item to remove.</param>
        /// <returns>How many of the item the inventory has.</returns>
        public int RemoveItem(ItemSO item, int amount)
        {
            int stackIndex = GetStackIndex(item);
            if (stackIndex < 0) return 0;
            ItemStack stack = Stacks[stackIndex];
            // Remove from the stack.
            stack.Amount -= amount;
            // Remove empty stacks.
            if (stack.Amount <= 0) Stacks.RemoveAt(stackIndex);
            return stack.Amount;
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
