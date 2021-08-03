namespace TimeStranded.Attributes
{
    /// <summary>
    /// Stores information to permanently or temporarily modify an attribute.
    /// </summary>
    public class AttributeModifier
    {
        /// <summary>
        /// How much to modify the attribute by.
        /// </summary>
        public float Value = 1;

        /// <summary>
        /// How long in seconds the modifier will last. If 0, the modifier's effect is never removed.
        /// </summary>
        public float Lifetime = 0;

        /// <summary>
        /// The time remaining on the modifier.
        /// </summary>
        public float TimeRemaining = 0;

        /// <summary>
        /// Creates an empty modifier.
        /// </summary>
        public AttributeModifier() { }

        /// <summary>
        /// Creates a modifier with the given value and lifetime.
        /// </summary>
        /// <param name="value">The modifier's value.</param>
        /// <param name="lifetime">The modifier's lifetime.</param>
        public AttributeModifier(float value, float lifetime)
        {
            Value = value;
            Lifetime = lifetime;
            TimeRemaining = Lifetime;
        }
    }
}
