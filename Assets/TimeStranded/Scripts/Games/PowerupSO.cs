using UnityEngine;
using TimeStranded.Attributes;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
    /// Stores data about a powerup.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPowerup", menuName = "Time Stranded/Games/Powerup")]
    public class PowerupSO : ItemSO
    {
        /// <summary>
        /// The attribute to affect.
        /// </summary>
        [Tooltip("The attribute to affect.")]
        public AttributeSO Attribute = null;

        /// <summary>
        /// How much to affect the attribute by.
        /// </summary>
        [Tooltip("How much to affect the attribute by.")]
        public float Value = 1;

        /// <summary>
        /// How long in seconds the powerup will last. If 0, the powerup's effect is never removed.
        /// </summary>
        [Tooltip("How long in seconds the powerup will last. If 0, the powerup's effect is never removed.")]
        public float Lifetime = 0;
    }
}
