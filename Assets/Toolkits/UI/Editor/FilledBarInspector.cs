using UnityEngine;
using UnityEditor;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="FilledBar"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(FilledBar))]
    [CanEditMultipleObjects]
    public class FilledBarInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The bar fill amount.
        /// </summary>
        private float _fillAmount = 1;

        public override void OnInspectorGUI()
        {
            int targetCount = targets.Length;
            FilledBar[] filledBars = new FilledBar[targetCount];

            for (int i = 0; i < targetCount; i++)
            {
                filledBars[i] = (FilledBar)targets[i];
            }

            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            EditorGUI.BeginChangeCheck();
            _fillAmount = EditorGUILayout.Slider("Fill Amount", _fillAmount, 0, 1);

            if (EditorGUI.EndChangeCheck())
            {
                for (int i = filledBars.Length - 1; i >= 0; i--)
                {
                    filledBars[i].SetFill(_fillAmount);
                }
            }
        }
    }
}
