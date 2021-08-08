using UnityEngine;
using Toolkits.Events;

namespace TimeStranded
{
    /// <summary>
    /// An event channel for <see cref="Interactable"/>s.
    /// </summary>
    [CreateAssetMenu(fileName = "NewInteractableEventChannel", menuName = "Time Stranded/Interactable Event Channel")]
    public class InteractableEventChannelSO : EventChannelSO<Interactable> { }
}
