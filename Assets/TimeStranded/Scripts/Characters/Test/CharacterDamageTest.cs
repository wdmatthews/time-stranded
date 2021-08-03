using UnityEngine;

namespace TimeStranded.Characters.Test
{
    /// <summary>
    /// Used to test characters taking damage.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Test/Character Damage Test")]
    [DisallowMultipleComponent]
    public class CharacterDamageTest : MonoBehaviour
    {
        /// <summary>
        /// The event channel to raise when a character is damaged.
        /// </summary>
        [Tooltip("The event channel to raise when a character is damaged.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDamage = null;

        /// <summary>
        /// The event channel to raise when a character dies.
        /// </summary>
        [Tooltip("The event channel to raise when a character dies.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDeath = null;

        private void Start()
        {
            // Subscribe to the character events.
            _onCharacterDamage.OnRaised += OnDamage;
            _onCharacterDeath.OnRaised += OnDeath;
        }

        /// <summary>
        /// Called when a character takes damage.
        /// </summary>
        /// <param name="character">The character that took damage.</param>
        private void OnDamage(Character character)
        {
            Debug.Log($"Character took damage. Remaining health: {character.AttributesByName["Health"].Value}.", character);
        }

        /// <summary>
        /// Called when a character dies.
        /// </summary>
        /// <param name="character">The character that died.</param>
        private void OnDeath(Character character)
        {
            Debug.Log("Character died.", character);
        }
    }
}
