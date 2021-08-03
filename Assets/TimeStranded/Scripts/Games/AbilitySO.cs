using UnityEngine;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
    /// Stores data about an ability.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAbility", menuName = "Time Stranded/Games/Ability")]
    public class AbilitySO : ItemSO
    {
        /// <summary>
        /// How long the ability takes to cooldown after it is used, before it can be used again.
        /// </summary>
        [Tooltip("How long the ability takes to cooldown after it is used, before it can be used again.")]
        public float Cooldown = 1;

        /// <summary>
        /// Whether or not the item can be selected.
        /// For example, a ball cannot be selected,
        /// but it can replace whatever ability the character selected.
        /// </summary>
        public override bool CanBeSelected => true;
    }
}
