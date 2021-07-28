using System.Collections.Generic;
using UnityEngine;

namespace Toolkits.NodeEditor
{
    /// <summary>
    /// Stores data for use in a node editor.
    /// </summary>
    public abstract class BaseEditorDataSO : ScriptableObject
    {
        /// <summary>
        /// The list of all base nodes used by the editor.
        /// </summary>
        public List<BaseNodeData> Nodes = new List<BaseNodeData>();

        /// <summary>
        /// The list of all connections used by the editor.
        /// </summary>
        public List<NodeConnectionData> Connections = new List<NodeConnectionData>();
    }
}
