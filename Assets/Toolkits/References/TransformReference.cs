using UnityEngine;

namespace Toolkits.References
{
    /// <summary>
    /// Used to set the <see cref="Transform"/> of a <see cref="TransformReferenceSO"/>.
    /// </summary>
    [AddComponentMenu("Toolkits/References/Transform Reference")]
    [DisallowMultipleComponent]
    public class TransformReference : MonoBehaviour
    {
        /// <summary>
        /// The transform reference.
        /// </summary>
        [Tooltip("The transform reference.")]
        [SerializeField] private TransformReferenceSO _transformReference = null;

        private void Awake()
        {
            _transformReference.Transform = transform;
        }
    }
}
