using UnityEngine;
using UnityEditor;

namespace TimeStranded.Characters.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="Character"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(Character), true)]
    [CanEditMultipleObjects]
    public class CharacterInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The face name used to set a character's face.
        /// </summary>
        private string _faceName = "";

        /// <summary>
        /// The color name used to set a character's color.
        /// </summary>
        private string _colorName = "";

        public override void OnInspectorGUI()
        {
            int targetCount = targets.Length;
            Character[] characters = new Character[targetCount];

            for (int i = 0; i < targetCount; i++)
            {
                characters[i] = (Character)targets[i];
            }

            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _faceName = EditorGUILayout.TextField("Face Name", _faceName);
            _colorName = EditorGUILayout.TextField("Color Name", _colorName);

            if (GUILayout.Button("Set Face"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    characters[i].SetFace(_faceName);
                }
            }

            if (GUILayout.Button("Set Color"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    characters[i].SetColor(_colorName);
                }
            }
        }
    }
}
