using UnityEngine;
using Toolkits.Variables;
using TimeStranded.Attributes;
using TimeStranded.Inventory;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Stores data about a character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Time Stranded/Characters/Character")]
    public class CharacterSO : ScriptableObject
    {
        /// <summary>
        /// The character's default face.
        /// </summary>
        [Tooltip("The character's default face.")]
        public CharacterFaceSO Face = null;

        /// <summary>
        /// The character's default color.
        /// </summary>
        [Tooltip("The character's default color.")]
        public CharacterColorSO Color = null;

        /// <summary>
        /// The name of the character's default face.
        /// </summary>
        [Tooltip("The name of the character's default face.")]
        public StringVariableSO FaceName = null;

        /// <summary>
        /// The name of the character's default color.
        /// </summary>
        [Tooltip("The name of the character's default color.")]
        public StringVariableSO ColorName = null;

        /// <summary>
        /// A collection of the character's attributes.
        /// </summary>
        [Tooltip("A collection of the character's attributes.")]
        public AttributeSO[] Attributes = { };

        /// <summary>
        /// The character's inventory.
        /// </summary>
        [Tooltip("The character's inventory.")]
        public InventorySO Inventory = null;

        /// <summary>
        /// The character's prefab.
        /// </summary>
        [Tooltip("The character's prefab.")]
        public Character Prefab = null;

        /// <summary>
        /// The character's prefab for arena use.
        /// </summary>
        [Tooltip("The character's prefab for arena use.")]
        public Character ArenaPrefab = null;

        /// <summary>
        /// The character's static prefab.
        /// </summary>
        [Tooltip("The character's static prefab.")]
        public StaticCharacter StaticPrefab = null;
    }
}
