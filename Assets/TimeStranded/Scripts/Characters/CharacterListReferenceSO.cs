using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Stores a list of characters with event raising on modification.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterList", menuName = "Time Stranded/Characters/Character List")]
    public class CharacterListReferenceSO : ListReferenceSO<Character>
    {
        /// <summary>
        /// The event channel to raise when a character is added.
        /// </summary>
        [Tooltip("The event channel to raise when a character is added.")]
        [SerializeField] private CharacterEventChannelSO _onCharacterAdded = null;

        /// <summary>
        /// The event channel to raise when a character is removed.
        /// </summary>
        [Tooltip("The event channel to raise when a character is removed.")]
        [SerializeField] private CharacterEventChannelSO _onCharacterRemoved = null;

        /// <summary>
        /// Adds a character to the list.
        /// </summary>
        /// <param name="character">The character to add.</param>
        public override void Add(Character character)
        {
            _onCharacterAdded.Raise(character);
            base.Add(character);
        }

        /// <summary>
        /// Removes a character from the list.
        /// </summary>
        /// <param name="character">The character to remove.</param>
        public override void Remove(Character character)
        {
            _onCharacterRemoved.Raise(character);
            base.Remove(character);
        }

        /// <summary>
        /// Removes a character from the list at the given index.
        /// </summary>
        /// <param name="index">The character's index.</param>
        public override void RemoveAt(int index)
        {
            _onCharacterRemoved.Raise(_list[index]);
            base.RemoveAt(index);
        }
    }
}
