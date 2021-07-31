using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Stores data about a character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Time Stranded/Characters/Character")]
    public class CharacterSO : ScriptableObject
    {
        /// <summary>
        /// The speed for moving the character.
        /// </summary>
        [Tooltip("The speed for moving the character.")]
        public float MoveSpeed = 1;

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
    }
}
