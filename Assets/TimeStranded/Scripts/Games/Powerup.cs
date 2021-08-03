using UnityEngine;
using TimeStranded.Attributes;
using TimeStranded.Characters;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
    /// Represents a powerup.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Powerup")]
    [DisallowMultipleComponent]
    public class Powerup : Item
    {
        /// <summary>
        /// The powerup's data.
        /// </summary>
        [Tooltip("The powerup's data.")]
        [SerializeField] protected PowerupSO _data = null;

        /// <summary>
        /// The powerup's renderer.
        /// </summary>
        [Tooltip("The powerup's renderer.")]
        [SerializeField] protected SpriteRenderer _renderer = null;

        private void Awake()
        {
            _renderer.sprite = _data.Sprite;
        }

        /// <summary>
        /// Sets the ball's velocity in the character's direction.
        /// </summary>
        /// <param name="character">The character using the ball.</param>
        public override void Use(MonoBehaviour character)
        {
            Character characterScript = (Character)character;
            characterScript.AttributesByName[_data.Attribute.Name]
                .ApplyModifier(new AttributeModifier(_data.Value, _data.Lifetime));
        }
    }
}
