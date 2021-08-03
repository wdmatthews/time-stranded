using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Locations
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="LocationSO"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLocationSceneLoadEventChannel", menuName = "Time Stranded/Locations/Location Scene Load Event Channel")]
    public class LocationSceneLoadEventChannelSO : EventChannelSO<LocationSO, float> { }
}
