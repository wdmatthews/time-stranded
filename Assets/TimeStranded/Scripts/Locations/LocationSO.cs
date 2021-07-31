using UnityEngine;

namespace TimeStranded.Locations
{
    /// <summary>
    /// Stores data about a location.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLocation", menuName = "Time Stranded/Locations/Location")]
    public class LocationSO : ScriptableObject
    {
        /// <summary>
        /// The path to the scene for the location.
        /// </summary>
        [Tooltip("The path to the scene for the location.")]
        public string ScenePath = "";
    }
}
