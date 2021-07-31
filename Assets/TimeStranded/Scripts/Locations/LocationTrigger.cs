using UnityEngine;

namespace TimeStranded.Locations
{
    /// <summary>
    /// Loads a location upon trigger enter.
    /// </summary>
    [AddComponentMenu("Time Stranded/Locations/Location Trigger")]
    [DisallowMultipleComponent]
    public class LocationTrigger : Trigger
    {
        /// <summary>
        /// The location manager to use.
        /// </summary>
        [Tooltip("The location manager to use.")]
        [SerializeField] private LocationManagerSO _locationManager = null;

        /// <summary>
        /// The location to load.
        /// </summary>
        [Tooltip("The location to load.")]
        [SerializeField] private LocationSO _location = null;

        /// <summary>
        /// Loads the location.
        /// </summary>
        /// <param name="collider">The collider that entered.</param>
        protected override void OnEnter(Collider2D collider)
        {
            _locationManager.LoadLocation(_location);
        }
    }
}
