using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Locations;

namespace TimeStranded.UI
{
    /// <summary>
    /// Manages the application of theme.
    /// </summary>
    [CreateAssetMenu(fileName = "NewThemeManager", menuName = "Time Stranded/UI/Theme Manager")]
    public class ThemeManagerSO : ScriptableObject
    {
        /// <summary>
        /// A collection of themes.
        /// </summary>
        [Tooltip("A collection of themes.")]
        [SerializeField] private ThemeSO[] _themes = { };

        /// <summary>
        /// The channel to raise when applying a theme.
        /// </summary>
        [Tooltip("The channel to raise when applying a theme.")]
        [SerializeField] private ThemeEventChannelSO _onThemeApplyChannel = null;

        /// <summary>
        /// The event channel to raise when a location finishes loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location finishes loading.")]
        [SerializeField] private LocationSceneLoadEventChannelSO _onLocationSceneLoadFinishChannel = null;

        /// <summary>
        /// The themes organized by name.
        /// </summary>
        [System.NonSerialized] private Dictionary<string, ThemeSO> _themesByName = new Dictionary<string, ThemeSO>();

        /// <summary>
        /// The theme currently in use.
        /// </summary>
        [System.NonSerialized] public ThemeSO Theme = null;

        /// <summary>
        /// Called on Awake.
        /// </summary>
        public void OnAwake()
        {
            // Load themes.
            for (int i = _themes.Length - 1; i >= 0; i--)
            {
                ThemeSO theme = _themes[i];
                _themesByName.Add(theme.name, theme);
            }

            // Subscribe to the location load to apply the current theme.
            _onLocationSceneLoadFinishChannel.OnRaised += OnLocationLoad;
        }

        /// <summary>
        /// Applies the current theme to the UI.
        /// </summary>
        private void ApplyCurrentTheme()
        {
            _onThemeApplyChannel.Raise(Theme);
        }

        /// <summary>
        /// Applies the given theme to the UI.
        /// </summary>
        /// <param name="themeName">The name of the theme to apply.</param>
        public void ApplyTheme(string themeName)
        {
            Theme = _themesByName[themeName];
            ApplyCurrentTheme();
        }

        /// <summary>
        /// Applies the current theme on location load if UI was loaded.
        /// </summary>
        /// <param name="location">The location that was loaded.</param>
        /// <param name="progress">The load progress.</param>
        private void OnLocationLoad(LocationSO location, float progress)
        {
            if (location.UIScenePath.Length == 0) return;
            ApplyCurrentTheme();
        }
    }
}
