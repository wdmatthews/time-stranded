using UnityEngine;
using UnityEngine.UI;

namespace Toolkits.UI
{
    /// <summary>
	/// A UI bar that can be filled to represent progress, health percentage, etc.
	/// </summary>
    [AddComponentMenu("Toolkits/UI/Filled Bar")]
    [DisallowMultipleComponent]
    public class FilledBar : MonoBehaviour
    {
        /// <summary>
        /// The bar's fill type.
        /// </summary>
        [Tooltip("The bar's fill type.")]
        [SerializeField] private FillType _fillType = FillType.FillAmount;

        /// <summary>
        /// The <see cref="RectTransform"/> containing the bar.
        /// </summary>
        [Tooltip("The RectTransform containing the bar.")]
        [SerializeField] private RectTransform _barTransform = null;

        /// <summary>
        /// The bar fill <see cref="RectTransform"/>.
        /// </summary>
        [Tooltip("The RectTransform containing the bar.")]
        [SerializeField] private RectTransform _fillTransform = null;

        /// <summary>
        /// The bar fill <see cref="Image"/>.
        /// </summary>
        [Tooltip("The bar fill Image.")]
        [SerializeField] private Image _fillImage = null;

        /// <summary>
        /// Sets the bar's fill amount.
        /// </summary>
        /// <param name="amount">A percentage represented by a number between 0-1 (0-100%).</param>
        public void SetFill(float amount)
        {
            if (_fillType == FillType.FillAmount)
            {
                _fillImage.fillAmount = amount;
            }
            else if (_fillType == FillType.Width)
            {
                _fillTransform.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Horizontal,
                    _barTransform.rect.width * amount
                );
            }
            else if (_fillType == FillType.Height)
            {
                _fillTransform.SetSizeWithCurrentAnchors(
                    RectTransform.Axis.Vertical,
                    _barTransform.rect.height * amount
                );
            }
        }
    }
}
