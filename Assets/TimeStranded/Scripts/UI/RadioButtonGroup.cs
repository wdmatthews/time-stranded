using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls a group of radio buttons.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Radio Button Group")]
    [DisallowMultipleComponent]
    public class RadioButtonGroup : MonoBehaviour
    {
        /// <summary>
        /// The group's toggle group.
        /// </summary>
        [Tooltip("The group's toggle group.")]
        public ToggleGroup ToggleGroup = null;

        /// <summary>
        /// The prefab to instantiate when initializing the button group.
        /// </summary>
        [Tooltip("The prefab to instantiate when initializing the button group.")]
        [SerializeField] private RadioButton _buttonPrefab = null;

        /// <summary>
        /// The event to raise when the group's value was changed.
        /// </summary>
        [Tooltip("The event to raise when the group's value was changed.")]
        [SerializeField] private UnityEvent<string> _onValueChanged = null;

        /// <summary>
        /// The theme manager.
        /// </summary>
        [Tooltip("The theme manager.")]
        [SerializeField] private ThemeManagerSO _themeManager = null;

        /// <summary>
        /// The group's value.
        /// </summary>
        private string _value = "";

        /// <summary>
        /// The group's value.
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChanged.Invoke(_value);
            }
        }

        /// <summary>
        /// The list of used radio buttons.
        /// </summary>
        private List<RadioButton> _radioButtons = new List<RadioButton>();

        /// <summary>
        /// Initializes the radio button group.
        /// </summary>
        /// <param name="values">The group's values.</param>
        /// <param name="icons">The icons for each button if needed.</param>
        /// <param name="colors">The colors for each button if needed.</param>
        public void Initialize(List<string> values, List<Sprite> icons = null, List<Color> colors = null)
        {
            int usedButtonCount = _radioButtons.Count;
            int valueCount = values.Count;
            _value = valueCount > 0 ? values[0] : "";

            // Create new buttons if needed, otherwise use old ones.
            for (int i = 0; i < valueCount; i++)
            {
                RadioButton button = null;

                if (i < usedButtonCount) button = _radioButtons[i];
                else
                {
                    button = Instantiate(_buttonPrefab, transform);
                    _radioButtons.Add(button);
                }

                button.Initialize(this, values[i], i == 0, colors != null ? colors[i] : new Color(1, 1, 1), icons?[i]);
                if (_themeManager) button.ApplyTheme(_themeManager.Theme);
                button.gameObject.SetActive(true);
            }

            // Disable any unused buttons.
            for (int i = usedButtonCount - 1; i >= valueCount; i--)
            {
                _radioButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
