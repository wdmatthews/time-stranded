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
        [SerializeField] protected BallSO _ballData = null;

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
            if (_ballData) SetData(_ballData);
        }

        private void LateUpdate()
        {
            // If the ball is moving, apply friction.
            if (_isMoving)
            {
                _rigidbody.velocity *= _ballData.Friction;

                // Detect if the ball is not moving enough, and stop it if so.
                if (_rigidbody.velocity.sqrMagnitude < _ballData.EffectivelyZeroSpeed)
                {
                    _isMoving = false;
                    _rigidbody.velocity = new Vector2();
                    // All the ball to be picked up when it stops moving.
                    CanBePickedUp = true;
                }
            }
        }

        /// <summary>
        /// Sets the ball's data.
        /// </summary>
        /// <param name="data">The ball's data.</param>
        public override void SetData(ItemSO data)
        {
            base.SetData(data);
            _ballData = (BallSO)data;
            _rigidbody.sharedMaterial = _ballData.PhysicsMaterial;
            _renderer.sprite = _ballData.Sprite;
        }

        /// <summary>
        /// Places the ball at the given position.
        /// </summary>
        /// <param name="position">The ball's new position.</param>
        public void Place(Vector3 position)
        {
            // Stop the ball's movement.
            SetVelocity(new Vector2());
            // Move the ball.
            transform.position = position;
        }

        /// <summary>
        /// Sets the ball's velocity in the given direction.
        /// </summary>
        /// <param name="direction">The direction for the ball to move in.</param>
        public void SetVelocity(Vector2 direction)
        {
            _isMoving = !Mathf.Approximately(direction.sqrMagnitude, 0);
            _rigidbody.velocity = _ballData.MoveSpeed * direction;
            CanBePickedUp = !_isMoving;
        }

        /// <summary>
        /// Sets the ball's velocity in the character's direction.
        /// </summary>
        /// <param name="character">The character using the ball.</param>
        /// <returns>If the item was used successfully or not.</returns>
        public override bool Use(MonoBehaviour character)
        {
            SetVelocity(character.transform.right);
            Character characterScript = (Character)character;
            characterScript.ReleaseItem();
            return true;
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

        /// <summary>
        /// Damages the character and reverses the ball's movement on hit.
        /// </summary>
        /// <param name="character">The character that the ball hit.</param>
        public override void OnHit(MonoBehaviour character)
        {
            // The ball should not damage or bounce if not moving.
            if (!_isMoving) return;

            // Damage the character if necessary.
            if (!Mathf.Approximately(_ballData.DamageOnHit, 0))
            {
                Character characterScript = (Character)character;
                // Do not damage the same team as the ball carrier.
                if (characterScript.Team.Length == 0 && Character != character
                    || Character && characterScript.Team != ((Character)Character).Team)
                {
                    characterScript.TakeDamage(_ballData.DamageOnHit, this);
                }
            }

            // Reverse the ball's movement with some random rotation.
            Bounce();
        }

        /// <summary>
        /// Bounces the ball away from its current velocity.
        /// </summary>
        public void Bounce()
        {
            Vector2 velocity = _rigidbody.velocity;
            float newAngle = Mathf.Rad2Deg * Mathf.Atan2(-velocity.y, -velocity.x)
                + Random.Range(-_ballData.BounceAngle, _ballData.BounceAngle);
            float newAngleInRadians = Mathf.Deg2Rad * newAngle;

            float speed = _rigidbody.velocity.magnitude;
            _rigidbody.velocity = new Vector2(speed * Mathf.Cos(newAngleInRadians),
                speed * Mathf.Sin(newAngleInRadians));
        }
    }
}
