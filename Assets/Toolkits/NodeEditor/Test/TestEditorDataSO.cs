using System.Collections.Generic;
using UnityEngine;

namespace Toolkits.NodeEditor.Test
{
    /// <summary>
    /// Stores data for the node editor test.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTestEditorData", menuName = "Toolkits/Node Editor/Test/Editor Data")]
    public class TestEditorDataSO : BaseEditorDataSO
    {
        /// <summary>
        /// The list of all test nodes used by the editor.
        /// </summary>
        public List<TestNodeData> TestNodes = new List<TestNodeData>();
    }
}
