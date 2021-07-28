using UnityEngine;
using UnityEditor;

namespace Toolkits.Audio.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing audio managers in the inspector.
    /// </summary>
    [CustomEditor(typeof(AudioManagerSO))]
    public class AudioManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The clip to play.
        /// </summary>
        private AudioClipSO _clip = null;

        /// <summary>
        /// The audio player used for playing music.
        /// </summary>
        private AudioPlayer _musicPlayer = null;

        public override void OnInspectorGUI()
        {
            AudioManagerSO audioManager = (AudioManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _clip = (AudioClipSO)EditorGUILayout.ObjectField("Audio Clip", _clip, typeof(AudioClipSO), false);

            if (GUILayout.Button("Play Clip"))
            {
                audioManager.Play(_clip);
            }

            if (GUILayout.Button("Play Music"))
            {
                if (_musicPlayer)
                {
                    _musicPlayer.FadeToClip(_clip);
                }
                else
                {
                    _musicPlayer = audioManager.Play(_clip);
                }
            }

            bool musicPlayerIsPlaying = _musicPlayer && _musicPlayer.IsPlaying;

            if (_musicPlayer && musicPlayerIsPlaying && GUILayout.Button("Pause Music"))
            {
                _musicPlayer.Pause();
            }
            else if (_musicPlayer && !musicPlayerIsPlaying && GUILayout.Button("Resume Music"))
            {
                _musicPlayer.Resume();
            }
        }
    }
}
