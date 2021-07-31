using UnityEngine;

namespace Toolkits.References
{
    /// <summary>
    /// Stores a reference to a transform that will be set at runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTransformReference", menuName = "Toolkits/References/Transform Reference")]
    public class TransformReferenceSO : ScriptableObject
    {
        /// <summary>
        /// The reference to set at runtime.
        /// </summary>
        [System.NonSerialized] public Transform Transform = null;
    }
}
