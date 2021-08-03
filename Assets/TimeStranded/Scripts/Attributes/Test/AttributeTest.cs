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

        /// <summary>
        /// An instance of the attribute.
        /// </summary>
        private AttributeSO _attributeInstance = null;

        private void Start()
        {
            // Make a runtime copy of the attribute reference.
            _attributeInstance = _attribute.Copy();
            Debug.Log("Copied the attribute into a new instance.");

            // Change the value of the attribute instance.
            _attributeInstance.ChangeValue(1);
            Debug.Log("Instance value changed.");

            // Apply a temporary modifier.
            _attributeInstance.ApplyModifier(new AttributeModifier(1, 1));
            Debug.Log("Modifier applied.");

            // Compare the value of the reference and instance.
            Debug.Log($"Reference value: {_attribute.Value}. Instance value: {_attributeInstance.Value}.");
        }

        private void Update()
        {
            // Update the attribute's modifier timers.
            _attributeInstance.OnUpdate();
            Debug.Log($"The attribute's value is: {_attributeInstance.Value}.");
        }
    }
}
