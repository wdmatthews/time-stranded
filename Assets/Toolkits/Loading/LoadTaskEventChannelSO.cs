using UnityEngine;

namespace Toolkits.Loading
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="ILoadTask"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLoadTaskEventChannel", menuName = "Toolkits/Loading/Load Task Event Channel")]
    public class LoadTaskEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<ILoadTask, float> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="loadTask">The load task to pass when raising the event.</param>
        /// <param name="progress">The progress to pass when raising the event.</param>
        public void Raise(ILoadTask loadTask, float progress) => OnRaised?.Invoke(loadTask, progress);
    }
}
