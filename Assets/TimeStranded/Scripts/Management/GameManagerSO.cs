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
        /// The camera follow targets.
        /// </summary>
        [Tooltip("The camera follow targets.")]
        [SerializeField] private TransformListReferenceSO _cameraFollowTargets = null;

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
            _locationManager.OnLocationSceneLoadStartChannel.OnRaised += OnLocationLoadStart;
        }

        /// <summary>
        /// Called on Start.
        /// </summary>
        public void OnStart()
        {
            _locationManager.LoadLocation(_mainMenu);
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

        /// <summary>
        /// Called when a location is loaded.
        /// </summary>
        /// <param name="location">The location that was loaded.</param>
        /// <param name="progress">The load progress.</param>
        private void OnLocationLoadStart(LocationSO location, float progress)
        {
            // Clear the camera targets.
            _cameraFollowTargets.Clear();
        }
    }
}
