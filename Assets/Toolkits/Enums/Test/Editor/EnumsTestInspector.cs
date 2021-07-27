using UnityEngine;
using UnityEditor;

namespace Toolkits.Enums.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="ScriptableObject"/> enums in the inspector.
    /// </summary>
    [CustomEditor(typeof(EnumsTest))]
    public class EnumsTestInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EnumsTest test = (EnumsTest)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");

            if (GUILayout.Button("Compare Teams"))
            {
                if (test.Player1Team == test.Player2Team)
                {
                    Debug.Log("Player 1 is on the same team as player 2.");
                }
                else
                {
                    Debug.Log("Player 1 is on a different team than player 2.");
                }
            }

            if (GUILayout.Button("Log Team 1"))
            {
                test.Player1Team.LogNameAndColor();
            }

            if (GUILayout.Button("Log Team 2"))
            {
                test.Player2Team.LogNameAndColor();
            }
        }
    }
}
