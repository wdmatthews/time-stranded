using UnityEngine;
using Toolkits.Events;
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
        /// The event channel to raise when showing the screen transition.
        /// </summary>
        [Tooltip("The event channel to raise when showing the screen transition.")]
        [SerializeField] private IntEventChannelSO _onScreenTransitionShowChannel = null;

        /// <summary>
        /// The main menu location.
        /// </summary>
        [Tooltip("The main menu location.")]
        [SerializeField] private LocationSO _mainMenu = null;

        /// <summary>
        /// The event channel to raise when starting a new game.
        /// </summary>
        [Tooltip("The event channel to raise when starting a new game.")]
        [SerializeField] private EventChannelSO _onNewGameStart = null;

        /// <summary>
        /// The town location.
        /// </summary>
        [Tooltip("The town location.")]
        [SerializeField] private LocationSO _town = null;

        /// <summary>
        /// Called on Awake.
        /// </summary>
        public void OnAwake()
        {
            _onNewGameStart.OnRaised += OnNewGameStart;
        }

        /// <summary>
        /// Called on Start.
        /// </summary>
        public void OnStart()
        {
            _locationManager.LoadLocation(_mainMenu);
            _onScreenTransitionShowChannel.Raise(1);
            // TODO Load theme name from save file if there is one.
            _themeManager.ApplyTheme("Future");
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        private void OnNewGameStart()
        {
            _locationManager.LoadLocation(_town);
        }
    }
}
