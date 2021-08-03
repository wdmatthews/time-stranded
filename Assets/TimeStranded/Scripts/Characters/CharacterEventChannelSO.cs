using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="Character"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterEventChannel", menuName = "Time Stranded/Characters/Character Event Channel")]
    public class CharacterEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<Character> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="character">The character to pass when raising the event.</param>
        public void Raise(Character character) => OnRaised?.Invoke(character);
    }
}
