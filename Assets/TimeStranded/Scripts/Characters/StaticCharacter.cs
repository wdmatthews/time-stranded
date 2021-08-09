using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// A non-moving character base.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Static Character")]
    [DisallowMultipleComponent]
    public class StaticCharacter : MonoBehaviour
    {
        /// <summary>
        /// The character's data.
        /// </summary>
        [Tooltip("The character's data.")]
        public CharacterSO Data = null;

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
        /// The character's face.
        /// </summary>
        [Tooltip("The character's face.")]
        [SerializeField] protected SpriteRenderer _face = null;

        /// <summary>
        /// The character's fill.
        /// </summary>
        [Tooltip("The character's fill.")]
        [SerializeField] protected SpriteRenderer _fill = null;

        protected virtual void Awake()
        {
            if (Data) Initialize(Data);
        }

        /// <summary>
        /// Initializes the character with the given data.
        /// </summary>
        /// <param name="data">The character's data.</param>
        public virtual void Initialize(CharacterSO data)
        {
            Data = data;
            // Initialize the character's face and color.
            if (Data.Face) SetFace(Data.Face);
            else if (Data.FaceName) SetFace(Data.FaceName.Value);
            if (Data.Color) SetColor(Data.Color);
            else if (Data.ColorName) SetColor(Data.ColorName.Value);
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
