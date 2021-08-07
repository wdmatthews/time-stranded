using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TimeStranded.UI
{
    /// <summary>
    /// Styles and controls a radio button.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Radio Button")]
    [DisallowMultipleComponent]
    public class RadioButton : MonoBehaviour
    {
        /// <summary>
        /// The button's toggle.
        /// </summary>
        [Tooltip("The button's toggle.")]
        [SerializeField] private Toggle _toggle = null;

        /// <summary>
        /// The button's outline.
        /// </summary>
        [Tooltip("The button's outline.")]
        [SerializeField] private Image _outline = null;

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

        /// <summary>
        /// The button's value.
        /// </summary>
        [System.NonSerialized] private string _value = "";

        /// <summary>
        /// The group this radio button belongs to.
        /// </summary>
        [System.NonSerialized] private RadioButtonGroup _group = null;

        private void Awake()
        {
            _onThemeApplyChannel.OnRaised += ApplyTheme;
        }

        private void OnDestroy()
        {
            _onThemeApplyChannel.OnRaised -= ApplyTheme;
        }

        /// <summary>
        /// Initializes the radio button.
        /// </summary>
        /// <param name="group">The button's group.</param>
        /// <param name="stringValue">The button's value.</param>
        /// <param name="startSelected">Whether or not this is the first radio button in the group.</param>
        /// <param name="icon">The button's icon, if any.</param>
        public void Initialize(RadioButtonGroup group, string stringValue, bool startSelected, Sprite icon = null)
        {
            _group = group;
            _value = stringValue;
            _toggle.group = _group.ToggleGroup;
            _toggle.isOn = startSelected;
            if (_label) _label.text = _value;
            if (_image) _image.sprite = icon;
        }

        /// <summary>
        /// Applies the given theme to the button.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void ApplyTheme(ThemeSO theme)
        {
            _outline.sprite = theme.ButtonSprite;
            if (_label) _label.font = theme.BoldFont;
        }

        /// <summary>
        /// Tries to set the group's value.
        /// </summary>
        /// <param name="isSelected">Whether or not the button is selected.</param>
        public void OnValueChanged(bool isSelected)
        {
            if (isSelected) _group.Value = _value;
        }
    }
}
