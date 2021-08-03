using UnityEngine;
using UnityEditor;

namespace Toolkits.Saving.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="TestSaveManagerSO"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(TestSaveManagerSO))]
    public class TestSaveManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// Used in the inspector to locate the save file.
        /// </summary>
        private string _saveName = "TestSave";

        /// <summary>
        /// Used in the inspector to store string data.
        /// </summary>
        private string _stringData = "0";

        /// <summary>
        /// Used in the inspector to store integer data.
        /// </summary>
        private int _intData = 0;

        /// <summary>
        /// Used in the inspector to store Vector2 data.
        /// </summary>
        private Vector2 _vector2Data = new Vector2();

        public override void OnInspectorGUI()
        {
            TestSaveManagerSO saveManager = (TestSaveManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Edit Mode Testing");
            _saveName = EditorGUILayout.TextField("Save Name", _saveName);
            _stringData = EditorGUILayout.TextField("String Data", _stringData);
            _intData = EditorGUILayout.IntField("Integer Data", _intData);
            _vector2Data = EditorGUILayout.Vector2Field("Vector2 Data", _vector2Data);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load"))
            {
                TestSaveData saveData = saveManager.Load(_saveName);

                if (saveData != null)
                {
                    _stringData = saveData.StringData;
                    _intData = saveData.IntData;
                    _vector2Data = saveData.Vector2Data;
                }
                else Debug.Log($"No save data found for {_saveName}.");
            }

            if (GUILayout.Button("Save"))
            {
                saveManager.Save(_saveName, new TestSaveData
                {
                    StringData = _stringData,
                    IntData = _intData,
                    Vector2Data = _vector2Data
                });
            }

            if (GUILayout.Button("Delete"))
            {
                saveManager.DeleteSave(_saveName);
            }

            GUILayout.EndHorizontal();
        }
    }
}
