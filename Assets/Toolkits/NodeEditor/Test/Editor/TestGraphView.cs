using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace Toolkits.NodeEditor.Test.Editor
{
    /// <summary>
    /// The test <see cref="GraphView"/>.
    /// </summary>
    public class TestGraphView : BaseGraphView
    {
        /// <summary>
        /// Creates the <see cref="GraphView"/>, passing in the style sheet to use.
        /// </summary>
        /// <param name="styleSheet">The style sheet for the graph view.</param>
        public TestGraphView(StyleSheet styleSheet)
            : base(styleSheet) { }

        /// <summary>
        /// Adds a test node at the given position.
        /// </summary>
        /// <param name="position">The position to place the node.</param>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public TestNode AddTestNode(Vector2 position, TestNodeData data = null)
        {
            // Create the node.
            TestNode node = new TestNode(RemoveElement, _styleSheet, position, data);
            // Add the node to the GraphView.
            AddElement(node);
            return node;
        }

        /// <summary>
        /// Adds a test node from node data.
        /// </summary>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public TestNode AddTestNode(TestNodeData data)
        {
            TestNode node = AddTestNode(data.Position, data);
            node.Guid = data.Guid;
            return node;
        }
    }
}
