using UnityEngine;

namespace Toolkits.UI
{
    /// <summary>
    /// A UI window that can be opened or closed.
    /// </summary>
    [AddComponentMenu("Toolkits/UI/Window")]
    [DisallowMultipleComponent]
    public class Window : MonoBehaviour
    {
        /// <summary>
        /// This window's <see cref="RectTransform"/>.
        /// </summary>
        [Tooltip("This window's RectTransform.")]
        [SerializeField] protected RectTransform _rectTransform = null;

        /// <summary>
        /// The animation to play when opening. No animation will turn the <see cref="GameObject"/> on.
        /// </summary>
        [Tooltip("The animation to play when opening. No animation will turn the GameObject on.")]
        [SerializeField] protected AnimationSO _openAnimation = null;

        /// <summary>
        /// The animation to play when closing. No animation will turn the <see cref="GameObject"/> off.
        /// </summary>
        [Tooltip("The animation to play when closing. No animation will turn the GameObject off.")]
        [SerializeField] protected AnimationSO _closeAnimation = null;

        /// <summary>
        /// Opens the window.
        /// </summary>
        /// <param name="animate">Whether or not to play an opening animation.</param>
        public void Open(bool animate = true)
        {
            if (animate && _openAnimation) _openAnimation.Animate(_rectTransform);
            else if (!animate && _openAnimation) _openAnimation.Skip(_rectTransform);
            else gameObject.SetActive(true);
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        /// <param name="animate">Whether or not to play a closing animation.</param>
        public void Close(bool animate = true)
        {
            if (animate && _closeAnimation) _closeAnimation.Animate(_rectTransform);
            else if (!animate && _closeAnimation) _closeAnimation.Skip(_rectTransform);
            else gameObject.SetActive(false);
        }
    }
}
