using UnityEngine;
using TMPro;

namespace TimeStranded.UI
{
    /// <summary>
    /// Stores data about a UI theme.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTheme", menuName = "Time Stranded/UI/Theme")]
    public class ThemeSO : ScriptableObject
    {
        /// <summary>
        /// The regular font to use.
        /// </summary>
        [Tooltip("The regular font to use.")]
        public TMP_FontAsset RegularFont = null;

        /// <summary>
        /// The bold font to use.
        /// </summary>
        [Tooltip("The bold font to use.")]
        public TMP_FontAsset BoldFont = null;

        /// <summary>
        /// The button sprite.
        /// </summary>
        [Tooltip("The button sprite.")]
        public Sprite ButtonSprite = null;

        /// <summary>
        /// The button color.
        /// </summary>
        [Tooltip("The button color.")]
        public Color ButtonColor = new Color(1, 1, 1);

        /// <summary>
        /// The button text color.
        /// </summary>
        [Tooltip("The button text color.")]
        public Color ButtonTextColor = new Color(1, 1, 1);
    }
}
