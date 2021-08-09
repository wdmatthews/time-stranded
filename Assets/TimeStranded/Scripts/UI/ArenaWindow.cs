using UnityEngine;
using DG.Tweening;
using Toolkits.Events;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls the arena game mode selection window.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Arena Window")]
    [DisallowMultipleComponent]
    public class ArenaWindow : MonoBehaviour
    {
        /// <summary>
        /// The arena window's rect transform.
        /// </summary>
        [Tooltip("The arena window's rect transform.")]
        [SerializeField] private RectTransform _rectTransform = null;

        /// <summary>
        /// The duration of the open/close animation.
        /// </summary>
        [Tooltip("The duration of the open/close animation.")]
        [SerializeField] private float _openCloseAnimationDuration = 1;

        /// <summary>
        /// The event channel to raise when opening the arena window.
        /// </summary>
        [Tooltip("The event channel to raise when opening the arena window.")]
        [SerializeField] private EventChannelSO _onArenaWindowOpenChannel = null;

        /// <summary>
        /// The event channel to raise when pausing the game.
        /// </summary>
        [Tooltip("The event channel to raise when pausing the game.")]
        [SerializeField] private EventChannelSO _onPauseChannel = null;

        /// <summary>
        /// The event channel to raise when resuming the game.
        /// </summary>
        [Tooltip("The event channel to raise when resuming the game.")]
        [SerializeField] private EventChannelSO _onResumeChannel = null;

        private void Awake()
        {
            if (_onArenaWindowOpenChannel) _onArenaWindowOpenChannel.OnRaised += Open;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_onArenaWindowOpenChannel) _onArenaWindowOpenChannel.OnRaised -= Open;
        }

        /// <summary>
        /// Opens the arena window.
        /// </summary>
        private void Open()
        {
            gameObject.SetActive(true);
            _rectTransform.DOAnchorPosY(0, _openCloseAnimationDuration)
                .From(new Vector2(0, -_rectTransform.rect.height))
                .onComplete += _onPauseChannel.Raise;
        }

        /// <summary>
        /// Closes the arena window.
        /// </summary>
        public void Close()
        {
            _rectTransform.DOAnchorPosY(-_rectTransform.rect.height, _openCloseAnimationDuration)
                .From(new Vector2())
                .onComplete += Hide;
        }

        /// <summary>
        /// Hides the arena window.
        /// </summary>
        private void Hide()
        {
            _onResumeChannel?.Raise();
            gameObject.SetActive(false);
        }
    }
}
