using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        /// Stores a list of scene names to reduce garbage collection.
        /// </summary>
        [System.NonSerialized] private List<string> _sceneNames = new List<string>();

        /// <summary>
        /// Loads a new location.
        /// </summary>
        /// <param name="location">The location to load.</param>
        public void LoadLocation(LocationSO location)
        {
            // Subscribe to events if needed.
            if (!_subscribedToEvents)
            {
                _subscribedToEvents = true;
                if (_sceneLoadManager.OnLoadStartChannel) _sceneLoadManager.OnLoadStartChannel.OnRaised += OnLoadStart;
                if (_sceneLoadManager.OnLoadTickChannel) _sceneLoadManager.OnLoadTickChannel.OnRaised += OnLoadTick;
                if (_sceneLoadManager.OnLoadFinishChannel) _sceneLoadManager.OnLoadFinishChannel.OnRaised += OnLoadFinish;
            }

            // Determine the scenes to unload.
            if (CurrentLocation)
            {
                _sceneNames.Clear();

                if (CurrentLocation.ScenePath.Length > 0) _sceneNames.Add(CurrentLocation.ScenePath);
                if (CurrentLocation.UIScenePath.Length > 0
                    && location.UIScenePath != CurrentLocation.UIScenePath)
                {
                    _sceneNames.Add(CurrentLocation.UIScenePath);
                }

                _sceneLoadManager.UnloadScenes(_sceneNames);
            }

            // Determine the scenes to load.
            LocationSO previousLocation = CurrentLocation;
            CurrentLocation = location;
            _sceneNames.Clear();

            if (CurrentLocation.ScenePath.Length > 0) _sceneNames.Add(CurrentLocation.ScenePath);
            if (CurrentLocation.UIScenePath.Length > 0
                && (!previousLocation || previousLocation.UIScenePath != CurrentLocation.UIScenePath))
            {
                _sceneNames.Add(CurrentLocation.UIScenePath);
            }

            _sceneLoadManager.LoadScenes(_sceneNames);
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
            OnLocationSceneLoadStartChannel?.Raise(CurrentLocation, progress);
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
            OnLocationSceneLoadTickChannel?.Raise(CurrentLocation, progress);
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

            if (CurrentLocation.ScenePath.Length > 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByPath(CurrentLocation.ScenePath));
            }
            else if (CurrentLocation.UIScenePath.Length > 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByPath(CurrentLocation.UIScenePath));
            }

            OnLocationSceneLoadFinishChannel?.Raise(CurrentLocation, progress);
        }
    }
}
