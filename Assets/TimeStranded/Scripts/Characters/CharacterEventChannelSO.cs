using UnityEngine;
using Toolkits.EventSystem;

namespace TimeStranded.Characters
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="Character"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCharacterEventChannel", menuName = "Time Stranded/Characters/Character Event Channel")]
    public class CharacterEventChannelSO : EventChannelSO<Character> { }
}
