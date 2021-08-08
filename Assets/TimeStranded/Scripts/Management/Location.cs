using UnityEngine;
using Toolkits.Variables;

namespace TimeStranded.Management
{
    /// <summary>
    /// Controls spawning a location.
    /// </summary>
    [AddComponentMenu("Time Stranded/Management/Location")]
    [DisallowMultipleComponent]
    public class Location : MonoBehaviour
    {
        /// <summary>
        /// The game's storyline.
        /// </summary>
        [Tooltip("The game's storyline.")]
        [SerializeField] private StringVariableSO _storyline = null;

        /// <summary>
        /// The prefab for the future version of the location.
        /// </summary>
        [Tooltip("The prefab for the future version of the location.")]
        [SerializeField] private Transform _futurePrefab = null;

        /// <summary>
        /// The prefab for the past version of the location.
        /// </summary>
        [Tooltip("The prefab for the past version of the location.")]
        [SerializeField] private Transform _pastPrefab = null;

        private void Awake()
        {
            // Instantiate the correct version of the location, then destroy this GameObject.
            Transform version = null;
            if (_storyline.Value == "Future") version = Instantiate(_futurePrefab, transform);
            else if (_storyline.Value == "Past") version = Instantiate(_pastPrefab, transform);
            version.parent = null;
            Destroy(gameObject);
        }
    }
}
