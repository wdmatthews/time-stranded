using UnityEngine;

namespace Toolkits.Variables
{
    /// <summary>
    /// A string stored in a <see cref="ScriptableObject"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Toolkits/Variables/String")]
    public class StringVariableSO : VariableSO<string> { }
}
