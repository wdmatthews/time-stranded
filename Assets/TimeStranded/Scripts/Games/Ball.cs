using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
	/// Controls the physics of a ball.
	/// </summary>
    [AddComponentMenu("Time Stranded/Games/Ball")]
    [DisallowMultipleComponent]
    public class Ball : MonoBehaviour
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
        /// The ball's renderer.
        /// </summary>
        [Tooltip("The ball's renderer.")]
        [SerializeField] protected SpriteRenderer _renderer = null;

        /// <summary>
        /// Whether or not the ball is moving.
        /// </summary>
        private bool _isMoving = false;

        private void Awake()
        {
            // Set the physics material and sprite.
            _rigidbody.sharedMaterial = _data.PhysicsMaterial;
            _renderer.sprite = _data.Sprite;
        }

        protected void LateUpdate()
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
                }
            }
        }

        /// <summary>
        /// Sets the ball's movement direction.
        /// </summary>
        /// <param name="direction">The direction for the ball to move in.</param>
        public void SetDirection(Vector2 direction)
        {
            _isMoving = !Mathf.Approximately(direction.sqrMagnitude, 0);
            _rigidbody.velocity = _data.MoveSpeed * direction;
        }
    }
}
