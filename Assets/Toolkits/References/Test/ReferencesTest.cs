using UnityEngine;

namespace Toolkits.References.Test
{
    /// <summary>
    /// Used to test references.
    /// </summary>
    [AddComponentMenu("Toolkits/References/Test/Test")]
    [DisallowMultipleComponent]
    public class ReferencesTest : MonoBehaviour
    {
        /// <summary>
        /// The reference SO to store the camera transform in.
        /// </summary>
        [Tooltip("The reference to store the camera transform in.")]
        [SerializeField] private TransformReferenceSO _reference = null;

        /// <summary>
        /// The camera to store in a <see cref="TransformReferenceSO"/>.
        /// </summary>
        [Tooltip("The camera to store in a TransformReferenceSO.")]
        [SerializeField] private Camera _camera = null;

        private void Start()
        {
            // Store the camera reference.
            _reference.Reference = _camera.transform;
            Debug.Log("Reference to camera stored.", _reference.Reference);
        }
    }
}
