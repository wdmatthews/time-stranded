using UnityEngine;
using UnityEditor;

namespace TimeStranded.Games.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="Character"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(Ball), true)]
    [CanEditMultipleObjects]
    public class BallInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The direction to move the ball in.
        /// </summary>
        private Vector2 _direction = new Vector2();

        /// <summary>
        /// Used to toggle if the ball can do damage.
        /// </summary>
        private bool _canDealDamage = false;

        public override void OnInspectorGUI()
        {
            int targetCount = targets.Length;
            Ball[] balls = new Ball[targetCount];

            for (int i = 0; i < targetCount; i++)
            {
                balls[i] = (Ball)targets[i];
            }

            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _direction = EditorGUILayout.Vector2Field("Direction", _direction);

            if (GUILayout.Button("Set Velocity"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    balls[i].SetVelocity(_direction);
                }
            }

            EditorGUI.BeginChangeCheck();
            _canDealDamage = EditorGUILayout.Toggle("Can Deal Damage", _canDealDamage);

            if (EditorGUI.EndChangeCheck())
            {
                for (int i = 0; i < targetCount; i++)
                {
                    balls[i].CanDealDamage = _canDealDamage;
                }
            }
        }
    }
}
