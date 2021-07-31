using UnityEngine;

namespace TimeStranded.Locations.Test
{
    /// <summary>
    /// Used to test location scene loading.
    /// </summary>
    [AddComponentMenu("Time Stranded/Locations/Test/Test")]
    [DisallowMultipleComponent]
    public class LocationTest : MonoBehaviour
    {
        /// <summary>
        /// The location manager to test.
        /// </summary>
        [Tooltip("The location manager to test.")]
        [SerializeField] private LocationManagerSO _locationManager = null;

        private void Awake()
        {
            // Subscribe to the loading events.
            _locationManager.OnLocationSceneLoadStartChannel.OnRaised += OnLoadStart;
            _locationManager.OnLocationSceneLoadTickChannel.OnRaised += OnLoadTick;
            _locationManager.OnLocationSceneLoadFinishChannel.OnRaised += OnLoadFinish;
        }

        /// <summary>
        /// Called when starting to load a location.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void OnLoadStart(LocationSO location, float progress)
        {
            Debug.Log($"Starting to load {location.ScenePath}.");
        }

        /// <summary>
        /// Called during loading a location.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void OnLoadTick(LocationSO location, float progress)
        {
            Debug.Log($"Load progress for {location.ScenePath}: {Mathf.Round(progress * 100)}%.");
        }

        /// <summary>
        /// Called when finishing loading a location.
        /// </summary>
        /// <param name="location">The location to load.</param>
        /// <param name="progress">The load progress.</param>
        private void OnLoadFinish(LocationSO location, float progress)
        {
            Debug.Log($"Finished loading {location.ScenePath}.");
        }
    }
}
