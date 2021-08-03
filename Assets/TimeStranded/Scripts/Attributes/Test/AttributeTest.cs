using UnityEngine;

namespace TimeStranded.Attributes.Test
{
    /// <summary>
    /// Used to test attributes.
    /// </summary>
    [AddComponentMenu("Time Stranded/Attributes/Test")]
    [DisallowMultipleComponent]
    public class AttributeTest : MonoBehaviour
    {
        /// <summary>
        /// The attribute to test.
        /// </summary>
        [Tooltip("The attribute to test.")]
        [SerializeField] private AttributeSO _attribute = null;

        private void Start()
        {
            // Make a runtime copy of the attribute reference.
            AttributeSO attributeInstance = _attribute.Copy();
            Debug.Log("Copied the attribute into a new instance.");

            // Change the value of the attribute instance.
            attributeInstance.ChangeValue(3);
            Debug.Log("Instance value changed.");

            // Compare the value of the reference and instance.
            Debug.Log($"Reference value: {_attribute.Value}. Instance value: {attributeInstance.Value}.");
        }
    }
}
