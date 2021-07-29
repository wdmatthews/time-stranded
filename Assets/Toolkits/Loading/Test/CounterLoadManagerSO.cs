using UnityEngine;

namespace Toolkits.Loading.Test
{
    /// <summary>
    /// A load manager that makes it easy to count to a number.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCounterLoadManager", menuName = "Toolkits/Loading/Test/Counter Load Manager")]
    public class CounterLoadManagerSO : LoadManagerSO
    {
        /// <summary>
        /// Counts to a given number, logging that number to the console.
        /// </summary>
        /// <param name="start">The number to start counting from.</param>
        /// <param name="stop">The number to stop counting at.</param>
        public void Count(int start, int stop) => Load(new CounterTask(start, stop));
    }
}
