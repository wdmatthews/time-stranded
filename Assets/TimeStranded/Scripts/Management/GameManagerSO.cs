using UnityEngine;
using TimeStranded.Locations;
using TimeStranded.UI;

namespace TimeStranded.Management
{
    /// <summary>
    /// Manages the lower level elements of the game, such as scene loading.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameManager", menuName = "Time Stranded/Management/Game Manager")]
    public class GameManagerSO : ScriptableObject
    {
        /// <summary>
        /// The location manager.
        /// </summary>
        [Tooltip("The location manager.")]
        [SerializeField] private LocationManagerSO _locationManager = null;

        /// <summary>
        /// The theme manager.
        /// </summary>
        [Tooltip("The theme manager.")]
        [SerializeField] private ThemeManagerSO _themeManager = null;

        /// <summary>
        /// The main menu location.
        /// </summary>
        [Tooltip("The main menu location.")]
        [SerializeField] private LocationSO _mainMenu = null;

        /// <summary>
        /// Called on Awake.
        /// </summary>
        public void OnAwake()
        {
            
        }

        /// <summary>
        /// Called on Start.
        /// </summary>
        public void OnStart()
        {
            _locationManager.LoadLocation(_mainMenu);
            // TODO Load theme name from save file.
            _themeManager.ApplyTheme("Future");
        }
    }
}
