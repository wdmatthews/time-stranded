using UnityEngine;

namespace Toolkits.UI.Test
{
    /// <summary>
	/// Used to test <see cref="FilledBar"/>.
	/// </summary>
    [AddComponentMenu("Toolkits/UI/Test/Filled Bar Test")]
    [DisallowMultipleComponent]
    public class FilledBarTest : MonoBehaviour
    {
        /// <summary>
        /// The filled bars to test.
        /// </summary>
        [Tooltip("The filled bars to test.")]
        [SerializeField] private FilledBar[] _bars = { };

        private void Start()
        {
            for (int i = _bars.Length - 1; i >= 0; i--)
            {
                _bars[i].SetFill(Random.Range(0f, 1f), true);
            }
        }
    }
}
