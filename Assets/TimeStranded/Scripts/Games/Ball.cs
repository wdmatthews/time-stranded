using UnityEngine;
using TimeStranded.Characters;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
	/// Controls the physics of a ball.
	/// </summary>
    [AddComponentMenu("Time Stranded/Games/Ball")]
    [DisallowMultipleComponent]
    public class Ball : Item
    {
        /// <summary>
        /// The ball's data.
        /// </summary>
        [Tooltip("The ball's data.")]
        [SerializeField] protected BallSO _data = null;

        /// <summary>
        /// The ball's rigidbody.
        /// </summary>
        [Tooltip("The ball's rigidbody.")]
        [SerializeField] protected Rigidbody2D _rigidbody = null;

        /// <summary>
        /// The ball's collider.
        /// </summary>
        [Tooltip("The ball's collider.")]
        [SerializeField] protected Collider2D _collider = null;

        /// <summary>
        /// The ball's renderer.
        /// </summary>
        [Tooltip("The ball's renderer.")]
        [SerializeField] protected SpriteRenderer _renderer = null;

        /// <summary>
        /// Whether or not the ball is moving.
        /// </summary>
        protected bool _isMoving = false;

        private void Awake()
        {
            // Set the physics material and sprite.
            _rigidbody.sharedMaterial = _data.PhysicsMaterial;
            _renderer.sprite = _data.Sprite;
        }

        private void LateUpdate()
        {
            // If the ball is moving, apply friction.
            if (_isMoving)
            {
                _rigidbody.velocity *= _data.Friction;

                // Detect if the ball is not moving enough, and stop it if so.
                if (_rigidbody.velocity.sqrMagnitude < _data.EffectivelyZeroSpeed)
                {
                    _isMoving = false;
                    _rigidbody.velocity = new Vector2();
                    // All the ball to be picked up when it stops moving.
                    CanBePickedUp = true;
                }
            }
        }

        /// <summary>
        /// Sets the ball's velocity in the given direction.
        /// </summary>
        /// <param name="direction">The direction for the ball to move in.</param>
        public void SetVelocity(Vector2 direction)
        {
            _isMoving = !Mathf.Approximately(direction.sqrMagnitude, 0);
            _rigidbody.velocity = _data.MoveSpeed * direction;
            CanBePickedUp = !_isMoving;
        }

        /// <summary>
        /// Sets the ball's velocity in the character's direction.
        /// </summary>
        /// <param name="character">The character using the ball.</param>
        public override void Use(MonoBehaviour character)
        {
            SetVelocity(character.transform.right);
            Character characterScript = (Character)character;
            characterScript.ReleaseItem();
        }

        /// <summary>
        /// Turns off the rigidbody.
        /// </summary>
        /// <param name="character">The character that picked up the item.</param>
        public override void OnPickup(MonoBehaviour character)
        {
            base.OnPickup(character);
            _rigidbody.velocity = new Vector2();
            _rigidbody.simulated = false;
            _isMoving = false;
        }

        /// <summary>
        /// Turns on the rigidbody.
        /// </summary>
        /// <param name="character">The character that released the item.</param>
        public override void OnRelease(MonoBehaviour character)
        {
            base.OnRelease(character);
            _rigidbody.simulated = true;
        }
    }
}
