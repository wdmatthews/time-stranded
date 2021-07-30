using UnityEngine;

namespace Toolkits.UI
{
    /// <summary>
    /// A UI window that can be opened or closed.
    /// </summary>
    [AddComponentMenu("Toolkits/UI/Tabbed Window")]
    [DisallowMultipleComponent]
    public class TabbedWindow : Window
    {
        /// <summary>
        /// The tabs belonging to this window.
        /// </summary>
        [Tooltip("The tabs belonging to this window.")]
        [SerializeField] private Tab[] _tabs = null;

        /// <summary>
        /// The number of tabs.
        /// </summary>
        private int _tabCount = 0;

        /// <summary>
        /// The index of the current tab.
        /// </summary>
        private int _currentTabIndex = 0;

        private void Awake()
        {
            _tabCount = _tabs.Length;

            for (int i = 0; i < _tabCount; i++)
            {
                _tabs[i].Initialize(i, SelectTab);
            }
        }

        /// <summary>
        /// Selects a tab by index.
        /// </summary>
        /// <param name="tabIndex">The index of the tab to select.</param>
        /// <param name="animate">Whether or not the tabs should animation selection and deselection.</param>
        public void SelectTab(int tabIndex, bool animate = true)
        {
            _tabs[_currentTabIndex].Deselect(animate);
            _currentTabIndex = tabIndex;
            _tabs[_currentTabIndex].Select(animate);
        }

        /// <summary>
        /// Selects the previous tab.
        /// </summary>
        public void SelectPreviousTab()
        {
            int index = _currentTabIndex - 1;
            if (index < 0) index = _tabCount - 1;
            SelectTab(index);
        }

        /// <summary>
        /// Selects the next tab.
        /// </summary>
        public void SelectNextTab()
        {
            int index = _currentTabIndex + 1;
            if (index >= _tabCount) index = 0;
            SelectTab(index);
        }
    }
}
