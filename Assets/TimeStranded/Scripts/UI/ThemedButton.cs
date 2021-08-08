using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TimeStranded.UI
{
    /// <summary>
    /// Applies a theme to a button.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Themed Button")]
    [DisallowMultipleComponent]
    public class ThemedButton : MonoBehaviour
    {
        /// <summary>
        /// The button.
        /// </summary>
        [Tooltip("The button.")]
        [SerializeField] private Button _button = null;

        /// <summary>
        /// The button's image.
        /// </summary>
        [Tooltip("The button's image.")]
        [SerializeField] private Image _image = null;

        /// <summary>
        /// The button's label.
        /// </summary>
        [Tooltip("The button's label.")]
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
        /// Applies the given theme to the button.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void ApplyTheme(ThemeSO theme)
        {
            _image.sprite = theme.ButtonSprite;
            _image.color = theme.ButtonColor;

            _label.font = theme.BoldFont;

            SpriteState buttonSpriteState = _button.spriteState;
            buttonSpriteState.highlightedSprite = theme.ButtonSpritePressed;
            buttonSpriteState.pressedSprite = theme.ButtonSpritePressed;
            buttonSpriteState.selectedSprite = theme.ButtonSpritePressed;
            buttonSpriteState.disabledSprite = theme.ButtonSpriteDisabled;
            _button.spriteState = buttonSpriteState;
        }
    }
}
