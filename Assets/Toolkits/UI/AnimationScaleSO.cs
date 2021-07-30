using UnityEngine;
using DG.Tweening;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A scaling animation.
    /// </summary>
    [CreateAssetMenu(fileName = "NewScaleAnimation", menuName = "Toolkits/UI/Animation Scale")]
    public class AnimationScaleSO : AnimationSO
    {
        /// <summary>
        /// The number to scale to.
        /// </summary>
        [Tooltip("The number to scale to.")]
        [SerializeField] private float _targetScale = 1;

        /// <summary>
        /// The number to scale from.
        /// </summary>
        [Tooltip("The number to scale from.")]
        [SerializeField] private float _fromScale = 0;

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
        /// Scales the given transform.
        /// </summary>
        /// <param name="transform">The transform to scale.</param>
        public override void Animate(Transform transform)
        {
            if (_gameObjectStartActive) transform.gameObject.SetActive(true);
            var animation = transform.DOScale(_targetScale, _duration).From(_fromScale);
            if (_gameObjectEndInactive) animation.onComplete += () => transform.gameObject.SetActive(false);
        }

        /// <summary>
        /// Scales the given transform.
        /// </summary>
        /// <param name="rectTransform">The transform to scale.</param>
        public override void Animate(RectTransform rectTransform) => Animate(rectTransform.transform);

        /// <summary>
        /// Scales instantly to the target scale.
        /// </summary>
        /// <param name="transform">The transform to scale.</param>
        public override void Skip(Transform transform)
        {
            transform.gameObject.SetActive(!_gameObjectEndInactive);
            transform.localScale = new Vector3(_targetScale, _targetScale, _targetScale);
        }

        /// <summary>
        /// Scales instantly to the target scale.
        /// </summary>
        /// <param name="transform">The transform to scale.</param>
        public override void Skip(RectTransform rectTransform) => Skip(rectTransform.transform);
    }
}
