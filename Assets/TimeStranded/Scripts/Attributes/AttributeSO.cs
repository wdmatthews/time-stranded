using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded.Attributes
{
    /// <summary>
    /// Stores data about an attribute.
    /// Used by characters during runtime.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAttribute", menuName = "Time Stranded/Attributes/Attribute")]
    public class AttributeSO : ScriptableObject
    {
        /// <summary>
        /// The attribute's name. It can be different from the file name for script access.
        /// </summary>
        [Tooltip("The attribute's name. It can be different from the file name for script access.")]
        public string Name = "";

        /// <summary>
        /// The attribute's maximum value.
        /// </summary>
        [Tooltip("The attribute's maximum value.")]
        public float MaxValue = 1;

        /// <summary>
        /// The attribute's minimum value.
        /// </summary>
        [Tooltip("The attribute's minimum value.")]
        public float MinValue = 0;

        /// <summary>
        /// The attribute's current value.
        /// </summary>
        [System.NonSerialized] public float Value = 0;

        /// <summary>
        /// The list of modifiers changing the attribute's value.
        /// </summary>
        [System.NonSerialized] private List<AttributeModifier> _modifiers = new List<AttributeModifier>();

        /// <summary>
        /// Returns a copy of this attribute for runtime use.
        /// </summary>
        /// <returns>An attribute instance.</returns>
        public AttributeSO Copy()
        {
            AttributeSO copiedAttribute = CreateInstance<AttributeSO>();
            copiedAttribute.Name = Name;
            copiedAttribute.MaxValue = MaxValue;
            copiedAttribute.MinValue = MinValue;
            return copiedAttribute;
        }

        /// <summary>
        /// Changes the attribute's value by the given amount.
        /// </summary>
        /// <param name="amount">The amount to change the value by.</param>
        public void ChangeValue(float amount)
        {
            Value = Mathf.Clamp(Value + amount, MinValue, MaxValue);
        }

        /// <summary>
        /// Applies a modifier to the attribute.
        /// </summary>
        /// <param name="modifier">The modifier to apply.</param>
        public void ApplyModifier(AttributeModifier modifier)
        {
            ChangeValue(modifier.Value);

            if (!Mathf.Approximately(modifier.Lifetime, 0))
            {
                _modifiers.Add(modifier);
            }
        }

        /// <summary>
        /// Updates the modifiers' timers.
        /// </summary>
        public void OnUpdate()
        {
            for (int i = _modifiers.Count - 1; i >= 0; i--)
            {
                AttributeModifier modifier = _modifiers[i];
                modifier.TimeRemaining = Mathf.Clamp(modifier.TimeRemaining - Time.deltaTime, 0, modifier.Lifetime);

                if (Mathf.Approximately(modifier.TimeRemaining, 0))
                {
                    ChangeValue(-modifier.Value);
                    _modifiers.RemoveAt(i);
                }
            }
        }
    }
}
