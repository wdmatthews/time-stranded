using UnityEngine;
using UnityEngine.UI;

namespace Toolkits.UI
{
    /// <summary>
    /// A button that controls which tab of a <see cref="TabbedWindow"/> is shown.
    /// </summary>
    [AddComponentMenu("Toolkits/UI/Tab")]
    [DisallowMultipleComponent]
    public class Tab : MonoBehaviour
    {
        /// <summary>
        /// The content of this tab.
        /// </summary>
        [Tooltip("The content of this tab.")]
        [SerializeField] private Transform _content = null;

        /// <summary>
        /// The tab's image.
        /// </summary>
        [Tooltip("The tab's image.")]
        [SerializeField] private Image _image = null;

        /// <summary>
        /// The tab's button.
        /// </summary>
        [Tooltip("The tab's button.")]
        [SerializeField] private Button _button = null;

        /// <summary>
        /// The animation to play on selection.
        /// </summary>
        [Tooltip("The animation to play on selection.")]
        [SerializeField] private AnimationSO _selectAnimation = null;

        /// <summary>
        /// The animation to play on deselection.
        /// </summary>
        [Tooltip("The animation to play on deselection.")]
        [SerializeField] private AnimationSO _deselectAnimation = null;

        /// <summary>
        /// The tab's index.
        /// </summary>
        private int _index = 0;

        /// <summary>
        /// The SelectTab method.
        /// </summary>
        private System.Action<int, bool> _selectTab = null;

        /// <summary>
        /// Initializes the tab.
        /// </summary>
        /// <param name="index">The tab's index.</param>
        /// <param name="selectTab">The SelectTab method.</param>
        public void Initialize(int index, System.Action<int, bool> selectTab)
        {
            _index = index;
            _selectTab = selectTab;
            _button.onClick.AddListener(Select);
        }

        /// <summary>
        /// Selects the tab from the parent <see cref="TabbedWindow"/>.
        /// </summary>
        private void Select() => _selectTab(_index, true);

        /// <summary>
        /// Selects the tab.
        /// </summary>
        /// <param name="animate">Whether or not a selection animation should play.</param>
        public void Select(bool animate = true)
        {
            _content.gameObject.SetActive(true);
            if (animate && _selectAnimation) _selectAnimation.Animate(_image);
            else if (!animate && _selectAnimation) _selectAnimation.Skip(_image);
        }

        /// <summary>
        /// Deselects the tab.
        /// </summary>
        /// <param name="animate">Whether or not a deselection animation should play.</param>
        public void Deselect(bool animate = true)
        {
            _content.gameObject.SetActive(false);
            if (animate && _deselectAnimation) _deselectAnimation.Animate(_image);
            else if (!animate && _deselectAnimation) _deselectAnimation.Skip(_image);
        }
    }
}
