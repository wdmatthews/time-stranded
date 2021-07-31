using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// Stores a reference to a camera that will be set at runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCameraReference", menuName = "Time Stranded/Camera Reference")]
    public class CameraReferenceSO : ScriptableObject
    {
        /// <summary>
        /// The reference to set at runtime.
        /// </summary>
        [System.NonSerialized] public Camera Camera = null;
    }
}
