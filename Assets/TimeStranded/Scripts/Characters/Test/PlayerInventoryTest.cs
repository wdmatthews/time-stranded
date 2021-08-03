using UnityEngine;
using TimeStranded.Inventory;

namespace TimeStranded.Characters.Test
{
    /// <summary>
    /// Used to test player inventory items and abilities.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Test/Player Inventory Test")]
    [DisallowMultipleComponent]
    public class PlayerInventoryTest : MonoBehaviour
    {
        /// <summary>
        /// The player to test.
        /// </summary>
        [Tooltip("The player to test.")]
        [SerializeField] private Player _player = null;

        /// <summary>
        /// The ability to test.
        /// </summary>
        [Tooltip("The ability to test.")]
        [SerializeField] private ItemSO _ability = null;

        private void Start()
        {
            // Add the item to the player's inventory.
            ItemStack ability = _player.Data.Inventory.AddItem(_ability, 2);
            // Add the item to the player's list of abilities.
            _player.AddAbility(ability);
        }
    }
}
