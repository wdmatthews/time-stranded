using System.Collections;
using UnityEngine;

namespace Toolkits.Loading
{
    /// <summary>
    /// An abstraction that simplifies loading data.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLoadManager", menuName = "Toolkits/Loading/Load Manager")]
    public class LoadManagerSO : ScriptableObject
    {
        /// <summary>
        /// The event channel to raise when a load task is started.
        /// </summary>
        [Tooltip("The event channel to raise when a load task is started.")]
        public LoadTaskEventChannelSO OnLoadStartChannel = null;

        /// <summary>
        /// The event channel to raise when a load task is ticked.
        /// </summary>
        [Tooltip("The event channel to raise when a load task is ticked.")]
        public LoadTaskEventChannelSO OnLoadTickChannel = null;

        /// <summary>
        /// The event channel to raise when a load task is finished.
        /// </summary>
        [Tooltip("The event channel to raise when a load task is finished.")]
        public LoadTaskEventChannelSO OnLoadFinishChannel = null;

        /// <summary>
        /// The StartCoroutine method from the load manager.
        /// </summary>
        [System.NonSerialized] public System.Func<IEnumerator, Coroutine> StartCoroutine = null;

        /// <summary>
        /// Starts running the given load task.
        /// </summary>
        /// <param name="task">The task to run.</param>
        public void Load(ILoadTask task)
        {
            StartCoroutine(StartLoad(task));
        }

        /// <summary>
        /// Starts a load task.
        /// </summary>
        /// <param name="task">The task to start.</param>
        /// <returns>A coroutine.</returns>
        private IEnumerator StartLoad(ILoadTask task)
        {
            // Start the task and raise the event with no progress.
            task.Start();
            OnLoadStartChannel.Raise(task, 0);

            // Tick the task and get its current progress,
            // passing that through to the event channel.
            while (!task.IsDone)
            {
                float progress = task.OnTick();
                OnLoadTickChannel.Raise(task, progress);
                yield return null;
            }

            // Finish the task and raise the event with 100% progress.
            task.OnFinish();
            OnLoadFinishChannel.Raise(task, 1);
        }
    }
}
