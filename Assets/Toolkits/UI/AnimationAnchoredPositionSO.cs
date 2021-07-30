using UnityEngine;
using DG.Tweening;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// An anchored position animation.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAnchoredPositionAnimation", menuName = "Toolkits/UI/Animation Anchored Position")]
    public class AnimationAnchoredPositionSO : AnimationSO
    {
        /// <summary>
        /// The position to move to.
        /// </summary>
        [Tooltip("The position to move to.")]
        [SerializeField] private Vector2 _targetPosition = new Vector2();

        /// <summary>
        /// The target anchor min.
        /// </summary>
        [Tooltip("The target anchor min.")]
        [SerializeField] private Vector2 _targetAnchorMin = new Vector2();

        /// <summary>
        /// The target anchor max.
        /// </summary>
        [Tooltip("The target anchor max.")]
        [SerializeField] private Vector2 _targetAnchorMax = new Vector2();

        /// <summary>
        /// The target pivot.
        /// </summary>
        [Tooltip("The target pivot.")]
        [SerializeField] private Vector2 _targetPivot = new Vector2();

        /// <summary>
        /// The position to move from.
        /// </summary>
        [Tooltip("The position to move from.")]
        [SerializeField] private Vector2 _fromPosition = new Vector2();

        /// <summary>
        /// The from anchor min.
        /// </summary>
        [Tooltip("The from anchor min.")]
        [SerializeField] private Vector2 _fromAnchorMin = new Vector2();

        /// <summary>
        /// The from anchor max.
        /// </summary>
        [Tooltip("The from anchor max.")]
        [SerializeField] private Vector2 _fromAnchorMax = new Vector2();

        /// <summary>
        /// The from pivot.
        /// </summary>
        [Tooltip("The from pivot.")]
        [SerializeField] private Vector2 _fromPivot = new Vector2();

        /// <summary>
        /// The duration of the animation.
        /// </summary>
        [Tooltip("The duration of the animation.")]
        [SerializeField] private float _duration = 1;

        /// <summary>
        /// If true, set the <see cref="GameObject"/> to active at the start.
        /// </summary>
        [Tooltip("If true, set the GameObject to active at the start.")]
        [SerializeField] private bool _gameObjectStartActive = true;

        /// <summary>
        /// If true, set the <see cref="GameObject"/> to inactive at the end.
        /// </summary>
        [Tooltip("If true, set the GameObject to inactive at the end.")]
        [SerializeField] private bool _gameObjectEndInactive = false;

        /// <summary>
        /// Moves the given transform.
        /// </summary>
        /// <param name="rectTransform">The transform to move.</param>
        public override void Animate(RectTransform rectTransform)
        {
            if (_gameObjectStartActive) rectTransform.gameObject.SetActive(true);
            rectTransform.DOAnchorMin(_targetAnchorMin, _duration).From(_fromAnchorMin);
            rectTransform.DOAnchorMax(_targetAnchorMax, _duration).From(_fromAnchorMax);
            rectTransform.DOPivot(_targetPivot, _duration).From(_fromPivot);
            var animation = rectTransform.DOAnchorPos(_targetPosition, _duration).From(_fromPosition);
            if (_gameObjectEndInactive) animation.onComplete += () => rectTransform.gameObject.SetActive(false);
        }

        /// <summary>
        /// Moves instantly to the target position.
        /// </summary>
        /// <param name="rectTransform">The transform to move.</param>
        public override void Skip(RectTransform rectTransform)
        {
            rectTransform.gameObject.SetActive(!_gameObjectEndInactive);
            rectTransform.anchorMin = _targetAnchorMin;
            rectTransform.anchorMax = _targetAnchorMax;
            rectTransform.pivot = _targetPivot;
            rectTransform.anchoredPosition = _targetPosition;
        }
    }
}
