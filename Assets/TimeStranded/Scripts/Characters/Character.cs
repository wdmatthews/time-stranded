using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Provides controls for a character.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Character")]
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The character's data.
        /// </summary>
        [Tooltip("The character's data.")]
        [SerializeField] protected CharacterSO _data = null;

        /// <summary>
        /// The collection of character faces.
        /// </summary>
        [Tooltip("The collection of character faces.")]
        [SerializeField] protected CharacterFacesSO _faces = null;

        /// <summary>
        /// The collection of character colors.
        /// </summary>
        [Tooltip("The collection of character colors.")]
        [SerializeField] protected CharacterColorsSO _colors = null;

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
        /// The character's fill.
        /// </summary>
        [Tooltip("The character's fill.")]
        [SerializeField] protected SpriteRenderer _fill = null;

        /// <summary>
        /// The current movement direction.
        /// </summary>
        protected Vector2 _moveDirection = new Vector2();

        /// <summary>
        /// The current aim direction.
        /// </summary>
        protected Vector2 _aimDirection = new Vector2();

        private void Awake()
        {
            SetFace(_data.Face);
            SetColor(_data.Color);
        }

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

        /// <summary>
        /// Sets the character's face.
        /// </summary>
        /// <param name="faceName">The name of the face.</param>
        public void SetFace(string faceName)
        {
            _face.sprite = _faces[faceName].Sprite;
        }

        /// <summary>
        /// Sets the character's face.
        /// </summary>
        /// <param name="face">The face.</param>
        public void SetFace(CharacterFaceSO face)
        {
            _face.sprite = face.Sprite;
        }

        /// <summary>
        /// Sets the character's color.
        /// </summary>
        /// <param name="colorName">The name of the color.</param>
        public void SetColor(string colorName)
        {
            _fill.color = _colors[colorName].Color;
        }

        /// <summary>
        /// Sets the character's color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void SetColor(CharacterColorSO color)
        {
            _fill.color = color.Color;
        }
    }
}
