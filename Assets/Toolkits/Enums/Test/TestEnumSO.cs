using UnityEngine;

namespace Toolkits.Enums.Test
{
    /// <summary>
    /// An example <see cref="ScriptableObject"/> enum pattern.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTestEnum", menuName = "Toolkits/Enums/Test/Enum")]
    public class TestEnumSO : ScriptableObject
    {
        /// <summary>
        /// A color corresponding to this <see cref="TestEnumSO"/> instance.
        /// </summary>
        [Tooltip("A color corresponding to this TestEnumSO instance.")]
        public Color Color = Color.white;

        /// <summary>
        /// Logs the name and color of this enum.
        /// </summary>
        public void LogNameAndColor()
        {
            Debug.Log($"TestEnumSO called {name} with a color of {Color}.");
        }
    }
}
