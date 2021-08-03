using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using Toolkits.NodeEditor;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Dialogues.Editor
{
    /// <summary>
    /// A node editor for dialogues.
    /// </summary>
    public class DialogueEditorWindow : BaseNodeEditorWindow
    {
        /// <summary>
        /// The name of the editor window.
        /// </summary>
        protected override string Name => Data.name;

        /// <summary>
        /// The data used to store information used by the dialogue node editor.
        /// </summary>
        protected DialogueSO Data { get; set; } = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save message nodes.
        /// </summary>
        protected SerializedProperty _serializedMessageNodes = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save choice nodes.
        /// </summary>
        protected SerializedProperty _serializedChoiceNodes = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save the start message node guid.
        /// </summary>
        protected SerializedProperty _serializedStartMessageNodeGuid = null;

        /// <summary>
        /// The graph view to use in this node editor window.
        /// </summary>
        protected DialogueGraphView _dialogueGraphView = null;

        /// <summary>
        /// The toolbar's add message node button.
        /// </summary>
        protected ToolbarButton _toolbarAddMessageNodeButton = null;

        /// <summary>
        /// The toolbar's add choice node button.
        /// </summary>
        protected ToolbarButton _toolbarAddChoiceNodeButton = null;

        /// <summary>
        /// The message node data by guid, used in saving connections.
        /// </summary>
        protected Dictionary<string, SerializedProperty> _messageDataByGuid = null;

        /// <summary>
        /// The choice node data by guid, used in saving connections.
        /// </summary>
        protected Dictionary<string, SerializedProperty> _choiceDataByGuid = null;

        /// <summary>
        /// Called when an asset is opened to try opening the node editor.
        /// </summary>
        /// <param name="instanceId">The asset's instance ID.</param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(1)]
        public static bool ShowWindow(int instanceId, int line)
        {
            Object asset = EditorUtility.InstanceIDToObject(instanceId);
            if (!(asset is DialogueSO)) return false;

            DialogueEditorWindow window = GetWindow<DialogueEditorWindow>();
            window.minSize = window.MinSize;
            window._data = (BaseEditorDataSO)asset;
            window.Data = (DialogueSO)asset;
            window.titleContent.text = window.Name;
            window.Load();

            return false;
        }

        /// <summary>
        /// Returns a new <see cref="DialogueGraphView"/> with a style sheet.
        /// </summary>
        /// <returns>The new <see cref="DialogueGraphView"/>.</returns>
        protected override BaseGraphView CreateGraphView()
        {
            _dialogueGraphView = new DialogueGraphView(_styleSheet);
            return _dialogueGraphView;
        }

        /// <summary>
        /// Adds to the toolbar at the top of the node editor.
        /// </summary>
        protected override void CreateToolbar()
        {
            base.CreateToolbar();

            _toolbarAddMessageNodeButton = new ToolbarButton(() => _dialogueGraphView.AddMessageNode(new Vector2()));
            _toolbarAddMessageNodeButton.text = "Add Message Node";
            _toolbar.Add(_toolbarAddMessageNodeButton);
            _toolbarAddChoiceNodeButton = new ToolbarButton(() => _dialogueGraphView.AddChoiceNode(new Vector2()));
            _toolbarAddChoiceNodeButton.text = "Add Choice Node";
            _toolbar.Add(_toolbarAddChoiceNodeButton);
        }

        /// <summary>
        /// Loads the node editor from the data.
        /// </summary>
        protected override void Load()
        {
            base.Load();
            _serializedMessageNodes = _serializedEditorData.FindProperty(nameof(DialogueSO.MessageNodes));
            _serializedChoiceNodes = _serializedEditorData.FindProperty(nameof(DialogueSO.ChoiceNodes));
            _serializedStartMessageNodeGuid = _serializedEditorData.FindProperty(nameof(DialogueSO.StartMessageNodeGuid));
        }

        /// <summary>
        /// Creates nodes from the save data.
        /// </summary>
        protected override void LoadNodes()
        {
            foreach (MessageNodeData nodeData in Data.MessageNodes)
            {
                _dialogueGraphView.AddMessageNode(nodeData);
            }

            foreach (ChoiceNodeData nodeData in Data.ChoiceNodes)
            {
                _dialogueGraphView.AddChoiceNode(nodeData);
            }
        }

        /// <summary>
        /// Clears the nodes before saving.
        /// </summary>
        protected override void ClearData()
        {
            base.ClearData();
            _serializedMessageNodes.ClearArray();
            _serializedChoiceNodes.ClearArray();
            _serializedStartMessageNodeGuid.stringValue = "";
            _messageDataByGuid = new Dictionary<string, SerializedProperty>();
            _choiceDataByGuid = new Dictionary<string, SerializedProperty>();
        }

        /// <summary>
        /// Saves a node.
        /// </summary>
        /// <param name="node">The node to save.</param>
        protected override void SaveNode(Node node)
        {
            if (node is MessageNode)
            {
                MessageNode messageNode = (MessageNode)node;
                MessageNodeData messageNodeData = new MessageNodeData
                {
                    Guid = messageNode.Guid,
                    Position = messageNode.GetPosition().position,
                    Speaker = messageNode.Data.Speaker,
                    Content = messageNode.Data.Content,
                    IsStartMessage = messageNode.Data.IsStartMessage
                };

                if (messageNodeData.IsStartMessage && _serializedStartMessageNodeGuid.stringValue == "")
                {
                    _serializedStartMessageNodeGuid.stringValue = messageNodeData.Guid;
                }

                SerializedProperty serializedNode = InsertElementIntoSerializedArray(_serializedNodes);
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Guid)).stringValue = messageNodeData.Guid;
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Position)).vector2Value = messageNodeData.Position;
                SerializedProperty serializedMessageNode = InsertElementIntoSerializedArray(_serializedMessageNodes);
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.Guid)).stringValue = messageNodeData.Guid;
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.Position)).vector2Value = messageNodeData.Position;
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.Speaker)).objectReferenceValue = messageNodeData.Speaker;
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.Content)).stringValue = messageNodeData.Content;
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.IsStartMessage)).boolValue = messageNodeData.IsStartMessage;
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.NextId)).stringValue = "";
                serializedMessageNode.FindPropertyRelative(nameof(MessageNodeData.NextIsMessage)).boolValue = true;
                _messageDataByGuid.Add(messageNodeData.Guid, serializedMessageNode);
            }

            if (node is ChoiceNode)
            {
                ChoiceNode choiceNode = (ChoiceNode)node;
                ChoiceNodeData choiceNodeData = new ChoiceNodeData
                {
                    Guid = choiceNode.Guid,
                    Position = choiceNode.GetPosition().position
                };

                SerializedProperty serializedNode = InsertElementIntoSerializedArray(_serializedNodes);
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Guid)).stringValue = choiceNodeData.Guid;
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Position)).vector2Value = choiceNodeData.Position;
                SerializedProperty serializedChoiceNode = InsertElementIntoSerializedArray(_serializedChoiceNodes);
                serializedChoiceNode.FindPropertyRelative(nameof(ChoiceNodeData.Guid)).stringValue = choiceNodeData.Guid;
                serializedChoiceNode.FindPropertyRelative(nameof(ChoiceNodeData.Position)).vector2Value = choiceNodeData.Position;
                SerializedProperty serializedChoices = serializedChoiceNode.FindPropertyRelative(nameof(ChoiceNodeData.Choices));
                serializedChoices.ClearArray();
                _choiceDataByGuid.Add(choiceNodeData.Guid, serializedChoiceNode);

                for (int i = choiceNode.outputContainer.childCount - 1; i >= 0; i--)
                {
                    InsertElementIntoSerializedArray(serializedChoices);
                }
            }
        }

        /// <summary>
        /// Saves the connections between quests.
        /// </summary>
        /// <param name="connection"></param>
        protected override void SaveConnection(Edge connection)
        {
            base.SaveConnection(connection);
            BaseNode inputNode = (BaseNode)connection.input.node;
            BaseNode outputNode = (BaseNode)connection.output.node;

            if (inputNode is MessageNode && outputNode is MessageNode)
            {
                // Add the input as a next message id to the output.
                SerializedProperty outputMessageData = _messageDataByGuid[outputNode.Guid];
                outputMessageData.FindPropertyRelative(nameof(MessageNodeData.NextId)).stringValue = inputNode.Guid;
                outputMessageData.FindPropertyRelative(nameof(MessageNodeData.NextIsMessage)).boolValue = true;
            }
            else if (inputNode is ChoiceNode && outputNode is MessageNode)
            {
                // Add the input as a next choice id to the output.
                SerializedProperty outputMessageData = _messageDataByGuid[outputNode.Guid];
                outputMessageData.FindPropertyRelative(nameof(MessageNodeData.NextId)).stringValue = inputNode.Guid;
                outputMessageData.FindPropertyRelative(nameof(MessageNodeData.NextIsMessage)).boolValue = false;
            }
            else if (inputNode is MessageNode && outputNode is ChoiceNode)
            {
                // Add the input as a choice id to the output.
                SerializedProperty outputChoiceData = _choiceDataByGuid[outputNode.Guid];
                int choiceIndex = outputNode.outputContainer.IndexOf(connection.output);
                outputChoiceData.FindPropertyRelative(nameof(ChoiceNodeData.Choices))
                    .GetArrayElementAtIndex(choiceIndex).stringValue = inputNode.Guid;
            }
        }
    }
}
