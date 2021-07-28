namespace Toolkits.NodeEditor
{
    /// <summary>
    /// Stores a connection from one node to another.
    /// </summary>
    [System.Serializable]
    public class NodeConnectionData
    {
        /// <summary>
        /// The Guid of the input node.
        /// </summary>
        public string InputNodeGuid = "";

        /// <summary>
        /// The index of the input port.
        /// </summary>
        public int InputPortIndex = 0;

        /// <summary>
        /// The Guid of the output node.
        /// </summary>
        public string OutputNodeGuid = "";

        /// <summary>
        /// The index of the output port.
        /// </summary>
        public int OutputPortIndex = 0;
    }
}
