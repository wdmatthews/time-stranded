using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
    /// Teleports anything to the linked portal.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Portal")]
    [DisallowMultipleComponent]
    public class Portal : Trigger
    {
        /// <summary>
        /// The portal linked to this one.
        /// </summary>
        [Tooltip("The portal linked to this one.")]
        [SerializeField] private Transform _linkedPortal = null;

        /// <summary>
        /// How long before the portal can be used again.
        /// </summary>
        [Tooltip("How long before the portal can be used again.")]
        [SerializeField] private float _cooldownDuration = 1;

        /// <summary>
        /// Whether or not the portal is cooling.
        /// </summary>
        private bool _isCooling = false;

        /// <summary>
        /// How much time is left on the cooldown.
        /// </summary>
        private float _cooldownTimer = 0;

        private void Update()
        {
            if (!_isCooling) return;
            if (Mathf.Approximately(_cooldownTimer, 0)) EndCooldown();
            else _cooldownTimer = Mathf.Clamp(_cooldownTimer - Time.deltaTime, 0, _cooldownDuration);
        }

        /// <summary>
        /// Teleports the collider to the other portal and starts the cooldown.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnEnter(Collider2D collider)
        {
            if (_isCooling) return;
            _linkedPortal.GetComponent<Portal>().StartCooldown();
            collider.transform.position = _linkedPortal.position;
            collider.transform.eulerAngles = _linkedPortal.eulerAngles;
            StartCooldown();
        }

        /// <summary>
        /// Starts the cooldown.
        /// </summary>
        private void StartCooldown()
        {
            _isCooling = true;
            _cooldownTimer = _cooldownDuration;
        }

        /// <summary>
        /// Ends the cooldown.
        /// </summary>
        private void EndCooldown()
        {
            _isCooling = false;
        }
    }
}
