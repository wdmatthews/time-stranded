using UnityEngine;
using TimeStranded.Characters;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
    /// A base class for ability instances.
    /// </summary>
    public abstract class Ability : Item
    {
        /// <summary>
        /// The ability's data.
        /// </summary>
        protected AbilitySO _abilityData = null;

        /// <summary>
        /// How much time is left on the cooldown timer.
        /// </summary>
        protected float _cooldownTimer = 0;

        /// <summary>
        /// Whether or not the ability is cooling down.
        /// </summary>
        protected bool _isCooling = false;

        private void Update()
        {
            if (!_isCooling) return;
            // Cool down, and stop when done.
            _cooldownTimer = Mathf.Clamp(_cooldownTimer - Time.deltaTime, 0, _abilityData.Cooldown);
            if (Mathf.Approximately(_cooldownTimer, 0)) _isCooling = false;
        }

        /// <summary>
        /// Sets the ability's data.
        /// </summary>
        /// <param name="data">The ability's data.</param>
        public override void SetData(ItemSO data)
        {
            base.SetData(data);
            _abilityData = (AbilitySO)data;
        }

        /// <summary>
        /// Tries using the ability.
        /// </summary>
        /// <param name="character">The character using the ability.</param>
        /// <returns>If the item was used successfully or not.</returns>
        public override bool Use(MonoBehaviour character)
        {
            if (_isCooling) return false;
            // If able to, use the item and start cooling.
            _isCooling = true;
            _cooldownTimer = _abilityData.Cooldown;
            OnUse((Character)character);
            return true;
        }

        /// <summary>
        /// Called when using the ability.
        /// </summary>
        /// <param name="character">The character using the ability.</param>
        protected abstract void OnUse(Character character);
    }
}
