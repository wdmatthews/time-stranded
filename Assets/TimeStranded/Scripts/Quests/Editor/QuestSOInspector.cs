using UnityEngine;
using UnityEditor;

namespace TimeStranded.Quests.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="QuestSO"/>s in the inspector.
    /// </summary>
    [CustomEditor(typeof(QuestSO))]
    public class QuestSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The amount to change the quest progress by.
        /// </summary>
        private int _amount = 1;

        public override void OnInspectorGUI()
        {
            QuestSO quest = (QuestSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _amount = EditorGUILayout.IntField("Progress Change Amount", _amount);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Active")) quest.IsActive = true;
            if (GUILayout.Button("Set Inactive")) quest.IsActive = false;
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Increase Progress")) quest.IncreaseProgress(_amount);
            if (GUILayout.Button("Decrease Progress")) quest.DecreaseProgress(_amount);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset Progress")) quest.ResetProgress();
            if (GUILayout.Button("Complete")) quest.Complete();
            GUILayout.EndHorizontal();
        }
    }
}
