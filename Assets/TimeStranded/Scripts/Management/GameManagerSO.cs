using UnityEngine;
using Toolkits.Loading;
using TimeStranded.Locations;

namespace TimeStranded.Management
{
    /// <summary>
    /// Manages the lower level elements of the game, such as scene loading.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameManager", menuName = "Time Stranded/Management/Game Manager")]
    public class GameManagerSO : ScriptableObject
    {
        /// <summary>
        /// The scene manager.
        /// </summary>
        [Tooltip("The scene manager.")]
        [SerializeField] private SceneLoadManagerSO _sceneManager = null;

        /// <summary>
        /// The location manager.
        /// </summary>
        [Tooltip("The location manager.")]
        [SerializeField] private LocationManagerSO _locationManager = null;

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
            // TODO Implement saving and loading instead of this temporary solution.
            _sceneManager.LoadScenes(new string[] { "MainMenu" });
        }
    }
}
