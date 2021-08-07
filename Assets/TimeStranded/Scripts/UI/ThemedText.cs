using UnityEngine;
using TMPro;

namespace TimeStranded.UI
{
    /// <summary>
    /// Applies a theme to text.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Themed Text")]
    [DisallowMultipleComponent]
    public class ThemedText : MonoBehaviour
    {
        /// <summary>
        /// Whether or not the text is bold.
        /// </summary>
        [Tooltip("Whether or not the text is bold.")]
        [SerializeField] private bool _isBold = false;

        /// <summary>
        /// The label.
        /// </summary>
        [Tooltip("The label.")]
        [SerializeField] private TextMeshProUGUI _label = null;

        /// <summary>
        /// The channel to raise when applying a theme.
        /// </summary>
        [Tooltip("The channel to raise when applying a theme.")]
        [SerializeField] private ThemeEventChannelSO _onThemeApplyChannel = null;

        private void Awake()
        {
            _onThemeApplyChannel.OnRaised += ApplyTheme;
        }

        private void OnDestroy()
        {
            _onThemeApplyChannel.OnRaised -= ApplyTheme;
        }

        /// <summary>
        /// Applies the given theme to the text.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void ApplyTheme(ThemeSO theme)
        {
            _label.font = _isBold ? theme.BoldFont : theme.RegularFont;
        }
    }
}
