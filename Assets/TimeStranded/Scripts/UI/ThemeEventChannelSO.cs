using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.UI
{
    /// <summary>
    /// An event channel for <see cref="ThemeSO"/> events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewThemeEventChannel", menuName = "Time Stranded/UI/Theme Event Channel")]
    public class ThemeEventChannelSO : EventChannelSO<ThemeSO> { }
}
