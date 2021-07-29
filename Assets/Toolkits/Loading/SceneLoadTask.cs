using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toolkits.Loading
{
    /// <summary>
    /// A load task used to load scenes.
    /// </summary>
    public class SceneLoadTask : ILoadTask
    {
        /// <summary>
        /// Determines if the task is done.
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// The list of scene names to load or unload.
        /// </summary>
        private string[] _sceneNames = { };

        /// <summary>
        /// Whether or not the scenes need to be unloaded.
        /// </summary>
        private bool _unload = false;

        /// <summary>
        /// The number of scenes to load or unload.
        /// Used to cache the length and save method calls.
        /// </summary>
        private int _sceneCount = 0;

        /// <summary>
        /// The scene currently being loaded or unloaded.
        /// </summary>
        private int _currentSceneIndex = 0;

        /// <summary>
        /// The operation of the scene currently being loaded or unloaded.
        /// </summary>
        private AsyncOperation _currentSceneOperation = null;

        /// <summary>
        /// Creates a scene load task 
        /// </summary>
        /// <param name="sceneNames">The names of the scenes to load or unload.</param>
        /// <param name="unload">Whether or not the scenes need to be unloaded.</param>
        public SceneLoadTask(string[] sceneNames, bool unload)
        {
            _sceneNames = sceneNames;
            _unload = unload;
            _sceneCount = _sceneNames.Length;
        }

        /// <summary>
        /// Starts the task.
        /// </summary>
        public void Start()
        {
            StartLoadingCurrentScene();
        }

        /// <summary>
        /// Starts loading or unloading the current scene.
        /// </summary>
        private void StartLoadingCurrentScene()
        {
            string currentSceneName = _sceneNames[_currentSceneIndex];
            if (_unload) _currentSceneOperation = SceneManager.UnloadSceneAsync(currentSceneName);
            else _currentSceneOperation = SceneManager.LoadSceneAsync(currentSceneName, LoadSceneMode.Additive);
        }

        /// <summary>
        /// Called every iteration of the loading while loop.
        /// </summary>
        /// <returns>The loading progress of the task, from 0-1 (0-100%).</returns>
        public float OnTick()
        {
            if (_currentSceneOperation.isDone)
            {
                _currentSceneIndex++;
                if (_currentSceneIndex == _sceneCount) IsDone = true;
                else StartLoadingCurrentScene();
            }

            return (_currentSceneIndex + _currentSceneOperation.progress) / _sceneCount;
        }

        /// <summary>
        /// Called when the task is finished.
        /// </summary>
        public void OnFinish() { }
    }
}
