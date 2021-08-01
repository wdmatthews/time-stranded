namespace TimeStranded.Inventory
{
    /// <summary>
    /// Stores an item and amount of that item.
    /// </summary>
    [System.Serializable]
    public class ItemStack
    {
        /// <summary>
        /// The item.
        /// </summary>
        public ItemSO Item = null;

        /// <summary>
        /// How much of the item is in the stack.
        /// </summary>
        public int Amount = 0;

        /// <summary>
        /// Creates an empty item stack.
        /// </summary>
        public ItemStack() { }

        /// <summary>
        /// Creates an item stack with the given item and amount.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="amount">How much of the item is in the stack.</param>
        public ItemStack(ItemSO item, int amount)
        {
            Item = item;
            Amount = amount;
        }

        /// <summary>
        /// Creates an item stack duplicate.
        /// </summary>
        /// <param name="stack">The stack to duplicate.</param>
        public ItemStack(ItemStack stack)
        {
            Item = stack.Item;
            Amount = stack.Amount;
        }
    }
}
