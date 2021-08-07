using UnityEngine;
using Toolkits.Events;

namespace TimeStranded
{
    /// <summary>
    /// An event channel for strings.
    /// </summary>
    [CreateAssetMenu(fileName = "NewStringEventChannel", menuName = "Time Stranded/String Event Channel")]
    public class StringEventChannelSO : EventChannelSO<string> { }
}
