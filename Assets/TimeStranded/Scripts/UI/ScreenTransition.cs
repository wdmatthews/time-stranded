using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls the UI of a screen transition.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Screen Transition")]
    [DisallowMultipleComponent]
    public class ScreenTransition : MonoBehaviour
    {
        /// <summary>
        /// The background for the screen transition.
        /// </summary>
        [Tooltip("The background for the screen transition.")]
        [SerializeField] private Image _background = null;

        /// <summary>
        /// How long the transition in or out lasts.
        /// </summary>
        [Tooltip("How long the transition in or out lasts.")]
        [SerializeField] private float _transitionDuration = 1;

        /// <summary>
        /// The event channel to raise when showing the screen transition.
        /// </summary>
        [Tooltip("The event channel to raise when showing the screen transition.")]
        [SerializeField] private IntEventChannelSO _onScreenTransitionChannel = null;

        private void Awake()
        {
            _onScreenTransitionChannel.OnRaised += Transition;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Starts the transition.
        /// </summary>
        /// <param name="start">The percentage from 0-1 to start at.</param>
        public void Transition(int start)
        {
            gameObject.SetActive(true);
            var animation = _background.DOFillAmount(1 - start, _transitionDuration).From(start);
            if (start == 1) animation.onComplete += FinishTransition;
        }

        /// <summary>
        /// Turns off the <see cref="GameObject"/>.
        /// </summary>
        private void FinishTransition() => gameObject.SetActive(false);
    }
}
