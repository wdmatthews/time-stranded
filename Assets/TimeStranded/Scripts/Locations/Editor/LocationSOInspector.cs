using UnityEditor;

namespace TimeStranded.Locations.Editor
{
    /// <summary>
    /// A custom editor to allow for referencing scene assets instead of names in <see cref="LocationSO"/>s.
    /// </summary>
    [CustomEditor(typeof(LocationSO))]
    public class LocationSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            LocationSO location = (LocationSO)target;
            // Get the old scene asset.
            SceneAsset oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(location.ScenePath);
            // Get the old UI scene asset.
            SceneAsset oldUIScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(location.UIScenePath);
            // Make sure the SerializedObject's data is up to date.
            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            // Show a SceneAsset object field.
            SceneAsset newScene = (SceneAsset)EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false);

            // If the scene was changed, get the new scene's path.
            if (EditorGUI.EndChangeCheck())
            {
                string newPath = AssetDatabase.GetAssetPath(newScene);
                SerializedProperty scenePathProperty = serializedObject.FindProperty(nameof(LocationSO.ScenePath));
                scenePathProperty.stringValue = newPath;
            }

            EditorGUI.BeginChangeCheck();
            // Show a SceneAsset object field.
            SceneAsset newUIScene = (SceneAsset)EditorGUILayout.ObjectField("UI Scene", oldUIScene, typeof(SceneAsset), false);

            // If the UI scene was changed, get the new UI scene's path.
            if (EditorGUI.EndChangeCheck())
            {
                string newUIPath = AssetDatabase.GetAssetPath(newUIScene);
                SerializedProperty uiScenePathProperty = serializedObject.FindProperty(nameof(LocationSO.UIScenePath));
                uiScenePathProperty.stringValue = newUIPath;
            }

            // Save the updated scene path.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
