using UnityEngine;

namespace TimeStranded.Locations
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="LocationSO"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLocationSceneLoadEventChannel", menuName = "Time Stranded/Locations/Location Scene Load Event Channel")]
    public class LocationSceneLoadEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<LocationSO, float> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="location">The location to pass when raising the event.</param>
        /// <param name="progress">The progress to pass when raising the event.</param>
        public void Raise(LocationSO location, float progress) => OnRaised?.Invoke(location, progress);
    }
}
