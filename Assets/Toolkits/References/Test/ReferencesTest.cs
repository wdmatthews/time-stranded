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
        /// The reference SO to load the camera transform from.
        /// </summary>
        [Tooltip("The reference to load the camera transform from.")]
        [SerializeField] private TransformReferenceSO _reference = null;

        private void Start()
        {
            // Log the camera reference.
            Debug.Log("Reference to camera stored.", _reference.Transform);
        }
    }
}
