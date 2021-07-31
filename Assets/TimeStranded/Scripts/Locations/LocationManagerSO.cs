using UnityEngine;
using Toolkits.Loading;

namespace TimeStranded.Locations
{
    /// <summary>
    /// Manages the current location and loading.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLocationManager", menuName = "Time Stranded/Locations/Location Manager")]
    public class LocationManagerSO : ScriptableObject
    {
        /// <summary>
        /// The scene load manager used to load locations.
        /// </summary>
        [Tooltip("The scene load manager used to load locations.")]
        [SerializeField] private SceneLoadManagerSO _sceneLoadManager = null;

        /// <summary>
        /// The event channel to raise when a location starts loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location starts loading.")]
        public LocationSceneLoadEventChannelSO OnLocationSceneLoadStartChannel = null;

        /// <summary>
        /// The event channel to raise when a location ticks during loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location ticks during loading.")]
        public LocationSceneLoadEventChannelSO OnLocationSceneLoadTickChannel = null;

        /// <summary>
        /// The event channel to raise when a location finishes loading.
        /// </summary>
        [Tooltip("The event channel to raise when a location finishes loading.")]
        public LocationSceneLoadEventChannelSO OnLocationSceneLoadFinishChannel = null;

        /// <summary>
        /// The current location.
        /// </summary>
        [System.NonSerialized] public LocationSO CurrentLocation = null;

        /// <summary>
        /// Whether or not the location manager subscribed to start, tick, and finish event channels.
        /// </summary>
        [System.NonSerialized] private bool _subscribedToEvents = false;

        /// <summary>
        /// Loads a new location.
        /// </summary>
        /// <param name="location">The location to load.</param>
        public void LoadLocation(LocationSO location)
        {
            if (!_subscribedToEvents)
            {
                _subscribedToEvents = true;
                _sceneLoadManager.OnLoadStartChannel.OnRaised += OnLoadStart;
                _sceneLoadManager.OnLoadTickChannel.OnRaised += OnLoadTick;
                _sceneLoadManager.OnLoadFinishChannel.OnRaised += OnLoadFinish;
            }

            if (CurrentLocation) _sceneLoadManager.UnloadScenes(new string[] { CurrentLocation.ScenePath });
            CurrentLocation = location;
            _sceneLoadManager.LoadScenes(new string[] { CurrentLocation.ScenePath });
        }

        /// <summary>
        /// Called when a location scene load task starts.
        /// </summary>
        /// <param name="task">The load task.</param>
        /// <param name="progress">The task progress.</param>
        private void OnLoadStart(ILoadTask task, float progress)
        {
            SceneLoadTask sceneTask = (SceneLoadTask)task;
            if (sceneTask.Unload) return;
            OnLocationSceneLoadStartChannel.Raise(CurrentLocation, progress);
        }

        /// <summary>
        /// Called when a location scene load task ticks.
        /// </summary>
        /// <param name="task">The load task.</param>
        /// <param name="progress">The task progress.</param>
        private void OnLoadTick(ILoadTask task, float progress)
        {
            SceneLoadTask sceneTask = (SceneLoadTask)task;
            if (sceneTask.Unload) return;
            OnLocationSceneLoadTickChannel.Raise(CurrentLocation, progress);
        }

        /// <summary>
        /// Called when a location scene load task finishes.
        /// </summary>
        /// <param name="task">The load task.</param>
        /// <param name="progress">The task progress.</param>
        private void OnLoadFinish(ILoadTask task, float progress)
        {
            SceneLoadTask sceneTask = (SceneLoadTask)task;
            if (sceneTask.Unload) return;
            OnLocationSceneLoadFinishChannel.Raise(CurrentLocation, progress);
        }
    }
}
