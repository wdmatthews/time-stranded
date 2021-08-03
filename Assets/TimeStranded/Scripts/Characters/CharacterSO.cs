using UnityEngine;
using TimeStranded.Attributes;

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
        /// A collection of the character's attributes.
        /// </summary>
        [Tooltip("A collection of the character's attributes.")]
        public AttributeSO[] Attributes = { };
    }
}
