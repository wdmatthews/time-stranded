using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolkits.NodeEditor.Editor
{
    /// <summary>
    /// A simplified base node.
    /// </summary>
    public class BaseNode : Node
    {
        /// <summary>
        /// The Guid used to identify this node in the data.
        /// </summary>
        public string Guid = System.Guid.NewGuid().ToString();

        /// <summary>
        /// The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.
        /// Used when removing connections, for example.
        /// </summary>
        protected System.Action<GraphElement> RemoveGraphViewElement = null;

        /// <summary>
        /// Creates a node with the given data.
        /// </summary>
        /// <param name="removeGraphViewElement">The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.</param>
        /// <param name="styleSheet">The style sheet used for the node.</param>
        /// <param name="position">The position for the node.</param>
        public BaseNode(System.Action<GraphElement> removeGraphViewElement, StyleSheet styleSheet, Vector2 position)
        {
            RemoveGraphViewElement = removeGraphViewElement;
            styleSheets.Add(styleSheet);
            SetPosition(new Rect(position, Vector2.zero));
        }

        /// <summary>
        /// Adds a port to the node.
        /// </summary>
        /// <param name="name">The name of the port, displayed next to the port.</param>
        /// <param name="direction">Whether the port is an input or output.</param>
        /// <param name="capacity">Whether the port supports one or multiple connection(s).</param>
        /// <param name="type">The type used by the port. float by default.</param>
        /// <returns>The added port.</returns>
        protected Port AddPort(string name, Direction direction,
            Port.Capacity capacity = Port.Capacity.Multi, System.Type type = null)
        {
            // Get the port and set its name.
            Port port = GetPort(direction, capacity, type ?? typeof(float));
            port.portName = name;

            // Add the port to the node.
            (direction == Direction.Input ? inputContainer : outputContainer).Add(port);
            RefreshExpandedState();
            RefreshPorts();
            return port;
        }

        /// <summary>
        /// Creates a port.
        /// </summary>
        /// <param name="direction">Whether the port is an input or output.</param>
        /// <param name="capacity">Whether the port supports one or multiple connection(s).</param>
        /// <param name="type">The type used by the port. float by default.</param>
        /// <returns>The created port.</returns>
        protected Port GetPort(Direction direction, Port.Capacity capacity = Port.Capacity.Multi,
            System.Type type = null)
        {
            return InstantiatePort(Orientation.Horizontal, direction, capacity, type ?? typeof(float));
        }

        /// <summary>
        /// Removes a port.
        /// </summary>
        /// <param name="port">The port to remove.</param>
        protected void RemovePort(Port port)
        {
            // Get all of the port's connections.
            Edge[] connections = (Edge[])port.connections;

            // Disconnect each side of every connection and remove the connection.
            foreach (Edge connection in connections)
            {
                connection.input.Disconnect(connection);
                connection.output.Disconnect(connection);
                RemoveGraphViewElement(connection);
            }
            
            // Remove the port.
            (port.direction == Direction.Input ? inputContainer : outputContainer).Remove(port);
            RefreshExpandedState();
            RefreshPorts();
        }

        /// <summary>
        /// Determines if a port on this node can connect to a port of the opposite direction on another node.
        /// <example>Example: Only allow the first output of an ExampleNode1 to connect to the first input of an ExampleNode2.
        /// <code>
        /// public override bool CanConnectToPort(Port startPort, BaseNode targetNode, Port targetPort)
        /// {
        ///     return this is ExampleNode1 &amp;&amp; targetNode is ExampleNode2
        ///         &amp;&amp; startPort.direction == Direction.Output &amp;&amp; outputContainer.IndexOf(startPort) == 0
        ///         &amp;&amp; targetPort.direction == Direction.Input &amp;&amp; targetNode.inputContainer.IndexOf(targetPort) == 0;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="startPort">The port on this node attempting to connect to another node.</param>
        /// <param name="targetNode">The target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <param name="targetPort">The port target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <returns>If the connection is allowed.</returns>
        public virtual bool CanConnectToPort(Port startPort, BaseNode targetNode, Port targetPort) => true;
    }
}
