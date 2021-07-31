using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Used as an enum to store a character color.
    /// </summary>
    [CreateAssetMenu(fileName = "NewColor", menuName = "Time Stranded/Characters/Color")]
    public class CharacterColorSO : ScriptableObject
    {
        /// <summary>
        /// The color.
        /// </summary>
        [Tooltip("The color.")]
        public Color Color = new Color(1, 1, 1);
    }
}
