using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Used as an enum to store a face sprite.
    /// </summary>
    [CreateAssetMenu(fileName = "NewFace", menuName = "Time Stranded/Characters/Face")]
    public class CharacterFaceSO : ScriptableObject
    {
        /// <summary>
        /// The face's sprite.
        /// </summary>
        [Tooltip("The face's sprite.")]
        public Sprite Sprite = null;
    }
}
