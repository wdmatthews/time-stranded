using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Dialogues.Editor
{
    /// <summary>
	/// A node representing multiple dialogue choices.
	/// </summary>
    public class ChoiceNode : BaseNode
    {
        /// <summary>
        /// Stores the data related to the choice node.
        /// </summary>
        public ChoiceNodeData Data { get; protected set; } = null;

        /// <summary>
        /// The ports used to represent choices.
        /// </summary>
        private List<Port> _outputs = new List<Port>();

        /// <summary>
        /// The button used to add a choice.
        /// </summary>
        private Button _addChoiceButton = null;

        /// <summary>
        /// Creates a choice node with the given data.
        /// </summary>
        /// <param name="removeGraphViewElement">The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.</param>
        /// <param name="styleSheet">The style sheet used for the node.</param>
        /// <param name="position">The position for the node.</param>
        public ChoiceNode(System.Action<GraphElement> removeGraphViewElement, StyleSheet styleSheet, Vector2 position,
            ChoiceNodeData data = null)
            : base(removeGraphViewElement, styleSheet, data != null ? data.Position : position)
        {
            Data = data != null ? new ChoiceNodeData(data) : new ChoiceNodeData { Guid = Guid };
            title = "Choice";

            _addChoiceButton = new Button(AddChoice);
            _addChoiceButton.text = "Add Choice";
            mainContainer.Add(_addChoiceButton);

            AddPort("Previous", Direction.Input, Port.Capacity.Multi, typeof(int));
        }

        /// <summary>
        /// Adds a choice to the node.
        /// </summary>
        private void AddChoice()
        {
            Port output = AddPort("", Direction.Output, Port.Capacity.Single, typeof(int));
            _outputs.Add(output);

            Button removeButton = new Button(() =>
            {
                RemoveChoice(output);
            });
            removeButton.text = "Remove";
            output.Add(removeButton);
        }

        /// <summary>
        /// Removes a choice by port.
        /// </summary>
        /// <param name="choicePort">The choice's port.</param>
        private void RemoveChoice(Port choicePort)
        {
            RemovePort(choicePort);
            _outputs.Remove(choicePort);
        }

        /// <summary>
        /// Makes sure that choice nodes can only connect to message nodes.
        /// </summary>
        /// <param name="startPort">The port on this node attempting to connect to another node.</param>
        /// <param name="targetNode">The target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <param name="targetPort">The port target node that <paramref name="startPort"/> is attempting to connect to.</param>
        /// <returns>If the connection is allowed.</returns>
        public override bool CanConnectToPort(Port startPort, BaseNode targetNode, Port targetPort)
        {
            return targetNode is MessageNode;
        }
    }
}
