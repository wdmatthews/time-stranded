using UnityEngine;

namespace Toolkits.Loading.Test
{
    /// <summary>
    /// A load task that counts to a number.
    /// </summary>
    public class CounterTask : ILoadTask
    {
        /// <summary>
        /// Determines if the task is done.
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// The number to start counting from.
        /// </summary>
        private int _start = 0;

        /// <summary>
        /// The number to stop counting at.
        /// </summary>
        private int _stop = 0;

        /// <summary>
        /// The current number.
        /// </summary>
        private int _current = 0;

        /// <summary>
        /// Creates a counter task with start and stop numbers.
        /// </summary>
        /// <param name="start">The number to start counting from.</param>
        /// <param name="stop">The number to stop counting at.</param>
        public CounterTask(int start, int stop)
        {
            _start = start;
            _stop = stop;
            _current = start;
        }

        /// <summary>
        /// Starts the task.
        /// </summary>
        public void Start()
        {
            Debug.Log($"Started counting from {_start}.");
        }

        /// <summary>
        /// Called every iteration of the loading while loop.
        /// </summary>
        /// <returns>The loading progress of the task, from 0-1 (0-100%).</returns>
        public float OnTick()
        {
            // Increment the current number.
            _current++;
            // Detect if the counting is done.
            if (_current == _stop) IsDone = true;
            Debug.Log($"Current number: {_current}");
            return 1f * (_current - _start) / (_stop - _start);
        }

        /// <summary>
        /// Called when the task is finished.
        /// </summary>
        public void OnFinish()
        {
            Debug.Log($"Stopped counting at {_stop}.");
        }
    }
}
