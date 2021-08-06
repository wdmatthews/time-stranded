using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

namespace TimeStranded.Games
{
    /// <summary>
    /// A wall that phases in and out.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Phase Wall")]
    [DisallowMultipleComponent]
    public class PhaseWall : MonoBehaviour
    {
        /// <summary>
        /// The wall's data.
        /// </summary>
        [Tooltip("The wall's data.")]
        [SerializeField] private PhaseWallSO _data = null;

        /// <summary>
        /// The wall's tilemap.
        /// </summary>
        [Tooltip("The wall's tilemap.")]
        [SerializeField] private Tilemap _tilemap = null;

        /// <summary>
        /// The wall's collider.
        /// </summary>
        [Tooltip("The wall's collider.")]
        [SerializeField] private Collider2D _collider = null;

        /// <summary>
        /// The current cooldown duration.
        /// </summary>
        private float _cooldownDuration = 0;

        /// <summary>
        /// The time left on the cooldown.
        /// </summary>
        private float _cooldownTimer = 0;

        /// <summary>
        /// Whether or not the wall is cooling.
        /// </summary>
        private bool _isCooling = false;

        /// <summary>
        /// Whether or not the wall is phased in.
        /// </summary>
        private bool _isPhasedIn = true;

        private void Start()
        {
            StartCooldown();
        }

        private void Update()
        {
            // Cooldown if needed.
            if (!_isCooling) return;
            if (Mathf.Approximately(_cooldownTimer, 0)) EndCooldown();
            else _cooldownTimer = Mathf.Clamp(_cooldownTimer - Time.deltaTime, 0, _cooldownDuration);
        }

        /// <summary>
        /// Starts the cooldown.
        /// </summary>
        private void StartCooldown()
        {
            _isCooling = true;
            _cooldownDuration = Random.Range(_data.MinCooldownDuration, _data.MaxCooldownDuration);
            _cooldownTimer = _cooldownDuration;
        }

        /// <summary>
        /// Ends the cooldown and starts phasing in or out again.
        /// </summary>
        private void EndCooldown()
        {
            _isCooling = false;
            _isPhasedIn = !_isPhasedIn;
            _collider.enabled = _isPhasedIn;
            DOTween.ToAlpha(() => _tilemap.color, color => _tilemap.color = color,
                _isPhasedIn ? 1 : _data.PhaseOutAlpha,
                _isPhasedIn ? _data.PhaseInDuration : _data.PhaseOutDuration);
            StartCooldown();
        }
    }
}
