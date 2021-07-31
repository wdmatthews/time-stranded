using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// Provides controls for a character.
    /// </summary>
    [AddComponentMenu("Time Stranded/Character")]
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The character's data.
        /// </summary>
        [Tooltip("The character's data.")]
        [SerializeField] protected CharacterSO _data = null;

        /// <summary>
        /// The character's rigibody.
        /// </summary>
        [Tooltip("The character's rigibody.")]
        [SerializeField] protected Rigidbody2D _rigidbody = null;

        /// <summary>
        /// The character's face.
        /// </summary>
        [Tooltip("The character's face.")]
        [SerializeField] protected SpriteRenderer _face = null;

        /// <summary>
        /// The current movement direction.
        /// </summary>
        protected Vector2 _moveDirection = new Vector2();

        /// <summary>
        /// The current aim direction.
        /// </summary>
        protected Vector2 _aimDirection = new Vector2();

        private void FixedUpdate()
        {
            _rigidbody.velocity = _data.MoveSpeed * _moveDirection;
        }

        /// <summary>
        /// Starts moving in the given direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        public void Move(Vector2 direction)
        {
            _moveDirection = direction;
        }

        /// <summary>
        /// Aims in the given direction.
        /// </summary>
        /// <param name="direction">The direction to aim in.</param>
        public void Aim(Vector2 direction)
        {
            _aimDirection = direction;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(_aimDirection.y, _aimDirection.x);
            transform.localEulerAngles = new Vector3(0, 0, angle);
            _face.transform.localEulerAngles = new Vector3(0, 0, -angle);
        }
    }
}
