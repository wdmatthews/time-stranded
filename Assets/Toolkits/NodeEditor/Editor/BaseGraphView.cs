using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolkits.NodeEditor.Editor
{
    /// <summary>
    /// A base <see cref="GraphView"/> for use in a node editor.
    /// </summary>
    public class BaseGraphView : GraphView
    {
        /// <summary>
        /// The style sheet to use in this <see cref="GraphView"/>.
        /// Passed from the node editor window.
        /// </summary>
        protected StyleSheet _styleSheet = null;

        /// <summary>
        /// The background for this <see cref="GraphView"/>.
        /// </summary>
        protected GridBackground _background = null;

        /// <summary>
        /// Creates the <see cref="GraphView"/>, passing in the style sheet to use.
        /// </summary>
        /// <param name="styleSheet">The style sheet for the graph view.</param>
        public BaseGraphView(StyleSheet styleSheet)
        {
            // Add the style sheet.
            _styleSheet = styleSheet;
            styleSheets.Add(_styleSheet);

            // Allow for zooming.
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            // Add any graph view manipulators. Access them through `this` because they are extentions.
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());

            // Add the background and stretch it to the GraphView's size.
            _background = new GridBackground();
            Insert(0, _background);
            _background.StretchToParentSize();

            // Stretch the GraphView to the node editor window's size. 
            this.StretchToParentSize();
        }

        /// <summary>
        /// Returns all of the ports that can be connected.
        /// </summary>
        /// <param name="startPort">The port that is attempting to connect to another.</param>
        /// <param name="nodeAdapter"></param>
        /// <returns></returns>
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> compatiblePorts = new List<Port>();

            foreach (Port port in ports)
            {
                BaseNode startNode = (BaseNode)startPort.node;
                BaseNode targetNode = (BaseNode)port.node;

                // The port is not allowed to connect to itself, its own node, or a port in the same direction,
                // along with any exceptions based on the node's type.
                if (startPort != port && startPort.node != port.node && startPort.direction != port.direction 
                    && startNode.CanConnectToPort(startPort, targetNode, port))
                {
                    compatiblePorts.Add(port);
                }
            }

            return compatiblePorts;
        }

        /// <summary>
        /// Adds a node at the given position.
        /// </summary>
        /// <param name="position">The position to place the node.</param>
        /// <returns>The added node.</returns>
        public BaseNode AddNode(Vector2 position)
        {
            // Create the node.
            BaseNode node = new BaseNode(RemoveElement, _styleSheet, position);
            // Add the node to the GraphView.
            AddElement(node);
            return node;
        }

        /// <summary>
        /// Adds a node from node data.
        /// </summary>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public BaseNode AddNode(BaseNodeData data)
        {
            BaseNode node = AddNode(data.Position);
            node.Guid = data.Guid;
            return node;
        }
    }
}
