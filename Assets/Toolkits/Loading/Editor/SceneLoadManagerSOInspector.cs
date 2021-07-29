using UnityEngine;
using UnityEditor;

namespace Toolkits.Loading.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="SceneLoadManagerSO"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(SceneLoadManagerSO))]
    public class SceneLoadManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// Used in the inspector to specify the first scene in the list.
        /// </summary>
        private string _scene1 = "";

        /// <summary>
        /// Used in the inspector to specify the second scene in the list.
        /// </summary>
        private string _scene2 = "";

        public override void OnInspectorGUI()
        {
            SceneLoadManagerSO sceneLoadManager = (SceneLoadManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _scene1 = EditorGUILayout.TextField("Scene 1", _scene1);
            _scene2 = EditorGUILayout.TextField("Scene 2", _scene2);
            string[] sceneNames = new string[] { _scene1, _scene2 };
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load"))
            {
                sceneLoadManager.LoadScenes(sceneNames);
            }

            if (GUILayout.Button("Unload"))
            {
                sceneLoadManager.UnloadScenes(sceneNames);
            }

            GUILayout.EndHorizontal();
        }
    }
}
