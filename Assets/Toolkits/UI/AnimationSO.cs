using UnityEngine;

namespace Toolkits.UI
{
    /// <summary>
    /// A base class for animations.
    /// </summary>
    public abstract class AnimationSO : ScriptableObject
    {
        /// <summary>
        /// Runs the animation on a transform.
        /// </summary>
        /// <param name="transform">The transform to animate.</param>
        public virtual void Animate(Transform transform) { }

        /// <summary>
        /// Runs the animation on a rect transform.
        /// </summary>
        /// <param name="transform">The transform to animate.</param>
        public virtual void Animate(RectTransform transform) { }

        /// <summary>
        /// Puts the transform in its final state, skipping the animation.
        /// </summary>
        /// <param name="transform">The transform to animate.</param>
        public virtual void Skip(Transform transform) { }

        /// <summary>
        /// Puts the rect transform in its final state, skipping the animation.
        /// </summary>
        /// <param name="rectTransform">The transform to animate.</param>
        public virtual void Skip(RectTransform rectTransform) { }
    }
}
