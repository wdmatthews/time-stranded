using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A color animation.
    /// </summary>
    [CreateAssetMenu(fileName = "NewColorAnimation", menuName = "Toolkits/UI/Animation Color")]
    public class AnimationColorSO : AnimationSO
    {
        /// <summary>
        /// The color to fade to.
        /// </summary>
        [Tooltip("The color to fade to.")]
        [SerializeField] private Color _targetColor = new Color(1, 1, 1);

        /// <summary>
        /// The color to fade from.
        /// </summary>
        [Tooltip("The color to fade from.")]
        [SerializeField] private Color _fromColor = new Color(0, 0, 0);

        /// <summary>
        /// The duration of the animation.
        /// </summary>
        [Tooltip("The duration of the animation.")]
        [SerializeField] private float _duration = 1;

        /// <summary>
        /// Colors the given image.
        /// </summary>
        /// <param name="image">The image to color.</param>
        public override void Animate(Image image)
        {
            image.DOColor(_targetColor, _duration).From(_fromColor);
        }

        /// <summary>
        /// Sets the image's color instantly to the target color.
        /// </summary>
        /// <param name="image">The image to color.</param>
        public override void Skip(Image image)
        {
            image.color = _targetColor;
        }
    }
}
