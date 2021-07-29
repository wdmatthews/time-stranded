namespace Toolkits.Loading
{
    /// <summary>
    /// A base interface for load tasks, such as loading a scene.
    /// </summary>
    public interface ILoadTask
    {
        /// <summary>
        /// Determines if the task is done.
        /// </summary>
        bool IsDone { get; set; }

        /// <summary>
        /// Starts the task.
        /// </summary>
        void Start();

        /// <summary>
        /// Called every iteration of the loading while loop.
        /// </summary>
        /// <returns>The loading progress of the task, from 0-1 (0-100%).</returns>
        float OnTick();

        /// <summary>
        /// Called when the task is finished.
        /// </summary>
        void OnFinish();
    }
}
