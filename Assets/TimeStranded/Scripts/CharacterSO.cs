using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// Stores data about a character.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Time Stranded/Character")]
    public class CharacterSO : ScriptableObject
    {
        /// <summary>
        /// The speed for moving the character.
        /// </summary>
        [Tooltip("The speed for moving the character.")]
        public float MoveSpeed = 1;
    }
}
