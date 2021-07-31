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

            // Save the updated scene path.
            serializedObject.ApplyModifiedProperties();
        }
    }
}
