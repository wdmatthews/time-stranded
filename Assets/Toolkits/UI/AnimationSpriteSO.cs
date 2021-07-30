using UnityEngine;
using UnityEngine.UI;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A sprite animation.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSpriteAnimation", menuName = "Toolkits/UI/Animation Sprite")]
    public class AnimationSpriteSO : AnimationSO
    {
        /// <summary>
        /// The sprite to switch to.
        /// </summary>
        [Tooltip("The number to scale to.")]
        [SerializeField] private Sprite _targetSprite = null;

        /// <summary>
        /// Sets the sprite of the given image.
        /// </summary>
        /// <param name="image">The image to set the sprite of.</param>
        public override void Animate(Image image)
        {
            image.sprite = _targetSprite;
        }

        /// <summary>
        /// Sets the image's sprite instantly to the target sprite.
        /// </summary>
        /// <param name="image">The image to set the sprite of.</param>
        public override void Skip(Image image)
        {
            image.sprite = _targetSprite;
        }
    }
}
