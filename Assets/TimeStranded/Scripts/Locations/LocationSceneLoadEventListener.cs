using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Locations
{
    /// <summary>
    /// A component that allows for location scene load events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Locations/Location Scene Load Event Listener")]
    public class LocationSceneLoadEventListener : EventListener<LocationSO, float> { }
}
