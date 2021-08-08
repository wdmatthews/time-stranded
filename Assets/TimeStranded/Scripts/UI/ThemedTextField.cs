using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TimeStranded.UI
{
    /// <summary>
    /// Applies a theme to a text field.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Themed Text Field")]
    [DisallowMultipleComponent]
    public class ThemedTextField : MonoBehaviour
    {
        /// <summary>
        /// The text field.
        /// </summary>
        [Tooltip("The text field.")]
        [SerializeField] private TMP_InputField _textField = null;

        /// <summary>
        /// The text field's image.
        /// </summary>
        [Tooltip("The text field's image.")]
        [SerializeField] private Image _image = null;

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
            _image.sprite = _textField.interactable ? theme.ButtonSprite : theme.ButtonSpriteDisabled;
            _image.color = theme.ButtonColor;

            _textField.fontAsset = theme.BoldFont;

            SpriteState textFieldSpriteState = _textField.spriteState;
            textFieldSpriteState.highlightedSprite = theme.ButtonSpritePressed;
            textFieldSpriteState.pressedSprite = theme.ButtonSpritePressed;
            textFieldSpriteState.selectedSprite = theme.ButtonSpritePressed;
            textFieldSpriteState.disabledSprite = theme.ButtonSpriteDisabled;
            _textField.spriteState = textFieldSpriteState;
        }
    }
}
