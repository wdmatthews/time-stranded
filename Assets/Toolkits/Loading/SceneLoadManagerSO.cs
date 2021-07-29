using UnityEngine;

namespace Toolkits.Loading
{
    /// <summary>
    /// A load manager that makes it easy to load and unload scenes additively.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSceneLoadManager", menuName = "Toolkits/Loading/Scene Load Manager")]
    public class SceneLoadManagerSO : LoadManagerSO
    {
        /// <summary>
        /// Loads the given scenes.
        /// </summary>
        /// <param name="sceneNames">The names of the scenes to load.</param>
        public void LoadScenes(string[] sceneNames) => Load(new SceneLoadTask(sceneNames, false));

        /// <summary>
        /// Unloads the given scenes.
        /// </summary>
        /// <param name="sceneNames">The names of the scenes to unload.</param>
        public void UnloadScenes(string[] sceneNames) => Load(new SceneLoadTask(sceneNames, true));
    }
}
