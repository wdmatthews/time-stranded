using UnityEngine;

namespace TimeStranded.Cameras
{
    /// <summary>
    /// Used to set the <see cref="Camera"/> of a <see cref="CameraReferenceSO"/>.
    /// </summary>
    [AddComponentMenu("Time Stranded/Cameras/Camera Reference")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraReference : MonoBehaviour
    {
        /// <summary>
        /// The camera.
        /// </summary>
        [Tooltip("The camera.")]
        [SerializeField] private Camera _camera = null;

        /// <summary>
        /// The camera reference.
        /// </summary>
        [Tooltip("The camera reference.")]
        [SerializeField] private CameraReferenceSO _cameraReference = null;

        private void Awake()
        {
            _cameraReference.Camera = _camera;
        }
    }
}
