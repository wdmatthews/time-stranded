using UnityEngine;

namespace TimeStranded.UI
{
    /// <summary>
    /// Provides the Awake method to a theme manager.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Theme Manager")]
    [DisallowMultipleComponent]
    public class ThemeManager : MonoBehaviour
    {
        /// <summary>
        /// The theme manager.
        /// </summary>
        [Tooltip("The theme manager.")]
        [SerializeField] private ThemeManagerSO _themeManager = null;

        private void Awake() => _themeManager.OnAwake();
    }
}
