using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace Toolkits.NodeEditor.Test.Editor
{
    public class TestNode : BaseNode
    {
        /// <summary>
        /// Stores the data related to the test node.
        /// </summary>
        public TestNodeData Data { get; protected set; } = null;

        /// <summary>
        /// The <see cref="TextField"/> used to change the test string in the data.
        /// </summary>
        private TextField _stringField = null;

        /// <summary>
        /// Creates a test node with the given data.
        /// </summary>
        /// <param name="removeGraphViewElement">The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.</param>
        /// <param name="styleSheet">The style sheet used for the node.</param>
        /// <param name="position">The position for the node.</param>
        public TestNode(System.Action<GraphElement> removeGraphViewElement, StyleSheet styleSheet, Vector2 position,
            TestNodeData data = null)
            : base(removeGraphViewElement, styleSheet, data != null ? data.Position : position)
        {
            // Create a new instance of the data that is not attached to the save data.
            Data = data != null ? new TestNodeData(data) : new TestNodeData { Guid = Guid };
            // Set the node's title to the test string.
            title = Data.TestString;

            // Add a text field to edit the test string.
            _stringField = new TextField { value = Data.TestString };
            _stringField.RegisterValueChangedCallback((value) =>
            {
                Data.TestString = value.newValue;
                title = Data.TestString;
            });
            mainContainer.Add(_stringField);

            // Add some test ports, using different types for coloring.
            AddPort("Special", Direction.Input, Port.Capacity.Single, typeof(int));
            AddPort("Special", Direction.Output, Port.Capacity.Single, typeof(int));
            AddPort("Single", Direction.Input, Port.Capacity.Single, typeof(float));
            AddPort("Single", Direction.Output, Port.Capacity.Single, typeof(float));
            AddPort("Multiple", Direction.Input, Port.Capacity.Multi, typeof(string));
            AddPort("Multiple", Direction.Output, Port.Capacity.Multi, typeof(string));
        }

        /// <summary>
        /// Allows any port connections, except special ports are only allowed to connect to special ports.
        /// </summary>
        /// <param name="startPort">The port on this node attempting to connect to another node.</param>
        /// <param name="targetNode">The target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <param name="targetPort">The port target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <returns>If the connection is allowed.</returns>
        public override bool CanConnectToPort(Port startPort, BaseNode targetNode, Port targetPort)
        {
            return startPort.portName == "Special" && targetPort.portName == "Special"
                || startPort.portName != "Special" && targetPort.portName != "Special";
        }
    }
}
