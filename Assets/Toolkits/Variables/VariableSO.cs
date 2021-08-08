using UnityEngine;

namespace Toolkits.Variables
{
    /// <summary>
	/// Stores a variable in a <see cref="ScriptableObject"/> for easy runtime reference.
	/// </summary>
	/// <typeparam name="T">The variable's type.</typeparam>
    public abstract class VariableSO<T> : ScriptableObject
    {
        /// <summary>
		/// The variable's value.
		/// </summary>
        public T Value { get; set; } = default;
    }
}
