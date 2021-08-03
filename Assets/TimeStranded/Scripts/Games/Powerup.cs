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
    public class Powerup : Ability
    {
        /// <summary>
        /// The powerup's data.
        /// </summary>
        [Tooltip("The powerup's data.")]
        [SerializeField] protected PowerupSO _powerupData = null;

        /// <summary>
        /// The powerup's renderer.
        /// </summary>
        [Tooltip("The powerup's renderer.")]
        [SerializeField] protected SpriteRenderer _renderer = null;

        private void Awake()
        {
            if (_powerupData) SetData(_powerupData);
        }

        /// <summary>
        /// Sets the powerup's data.
        /// </summary>
        /// <param name="data">The powerup's data.</param>
        public override void SetData(ItemSO data)
        {
            base.SetData(data);
            _powerupData = (PowerupSO)data;
            _renderer.sprite = _powerupData.Sprite;
        }

        /// <summary>
        /// Uses the powerup on the character.
        /// </summary>
        /// <param name="character">The character using the powerup.</param>
        protected override void OnUse(Character character)
        {
            character.AttributesByName[_powerupData.Attribute.Name]
                .ApplyModifier(new AttributeModifier(_powerupData.Value, _powerupData.Lifetime));
        }
    }
}
