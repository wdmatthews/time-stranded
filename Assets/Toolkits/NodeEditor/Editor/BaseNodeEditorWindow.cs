using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Toolkits.NodeEditor.Editor
{
    /// <summary>
    /// A base node <see cref="EditorWindow"/>.
    /// </summary>
    public class BaseNodeEditorWindow : EditorWindow
    {
        /// <summary>
        /// The name of the editor window.
        /// </summary>
        protected virtual string Name => "Node Editor";

        /// <summary>
        /// The minimum size of the editor window.
        /// </summary>
        protected virtual Vector2 MinSize => new Vector2(512, 512);

        /// <summary>
        /// The path to the style sheet used for this node editor.
        /// </summary>
        protected virtual string StyleSheetPath => "NodeEditorStyleSheet";

        /// <summary>
        /// The data used to store information used by the node editor.
        /// </summary>
        protected BaseEditorDataSO _data = null;

        /// <summary>
        /// The <see cref="SerializedObject"/> used to save data.
        /// </summary>
        protected SerializedObject _serializedEditorData = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save nodes.
        /// </summary>
        protected SerializedProperty _serializedNodes = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save connections.
        /// </summary>
        protected SerializedProperty _serializedConnections = null;

        /// <summary>
        /// The graph view to use in this node editor window.
        /// </summary>
        protected BaseGraphView _graphView = null;

        /// <summary>
        /// The style sheet to use in this node editor window.
        /// Passed to any elements that need styled.
        /// </summary>
        protected StyleSheet _styleSheet = null;

        /// <summary>
        /// The toolbar at the top of the node editor.
        /// </summary>
        protected Toolbar _toolbar = null;

        /// <summary>
        /// The toolbar's load button.
        /// </summary>
        protected ToolbarButton _toolbarLoadButton = null;

        /// <summary>
        /// The toolbar's save button.
        /// </summary>
        protected ToolbarButton _toolbarSaveButton = null;

        /// <summary>
        /// Called when an asset is opened to try opening the node editor.
        /// The below code is boilerplate, with an example shown in TestNodeEditorWindow.
        /// </summary>
        /// <param name="instanceId">The asset's instance ID.</param>
        /// <param name="line"></param>
        /// <returns></returns>
        // [OnOpenAsset(1)]
        // public static bool ShowWindow(int instanceId, int line)
        // {
        //     // Get the selected asset and check if it is the correct type.
        //     Object asset = EditorUtility.InstanceIDToObject(instanceId);
        //     if (!(asset is BaseEditorDataSO)) return false;

        //     // Open the node editor window and initialize its values.
        //     BaseNodeEditorWindow window = GetWindow<BaseNodeEditorWindow>();
        //     window.minSize = window.MinSize;
        //     window._data = (BaseEditorDataSO)asset;
        //     // Load the data.
        //     window.titleContent.text = window.Name;
        //     window.Load();

        //     return false;
        // }

        protected virtual void OnEnable()
        {
            // Apply the stylesheet.
            _styleSheet = Resources.Load<StyleSheet>(StyleSheetPath);
            rootVisualElement.styleSheets.Add(_styleSheet);

            // Create and add the GraphView.
            _graphView = CreateGraphView();
            rootVisualElement.Add(_graphView);

            // Add the toolbar.
            CreateToolbar();
        }

        protected virtual void OnDisable()
        {
            // Don't draw the GraphView if the window is not shown.
            rootVisualElement.Remove(_graphView);
        }

        /// <summary>
        /// Returns a new <see cref="BaseGraphView"/> with a style sheet.
        /// Can be overriden to return a new instance of a class that inherits from <see cref="BaseGraphView"/>.
        /// </summary>
        /// <returns>The new <see cref="BaseGraphView"/>.</returns>
        protected virtual BaseGraphView CreateGraphView()
        {
            return new BaseGraphView(_styleSheet);
        }

        /// <summary>
        /// Creates the toolbar at the top of the node editor.
        /// Can be overriden to add additional elements to the toolbar.
        /// </summary>
        protected virtual void CreateToolbar()
        {
            // Create the toolbar.
            _toolbar = new Toolbar();

            // Create the load button.
            _toolbarLoadButton = new ToolbarButton(Load);
            _toolbarLoadButton.text = "Load";
            _toolbar.Add(_toolbarLoadButton);

            // Create the save button.
            _toolbarSaveButton = new ToolbarButton(Save);
            _toolbarSaveButton.text = "Save";
            _toolbar.Add(_toolbarSaveButton);

            // Add the toolbar.
            rootVisualElement.Add(_toolbar);
        }

        /// <summary>
        /// Loads the node editor from the data.
        /// </summary>
        protected virtual void Load()
        {
            // Get the serialized object and its properties.
            _serializedEditorData = new SerializedObject(_data);
            _serializedNodes = _serializedEditorData.FindProperty(nameof(BaseEditorDataSO.Nodes));
            _serializedConnections = _serializedEditorData.FindProperty(nameof(BaseEditorDataSO.Connections));

            // Clear out all current nodes.
            foreach (Node node in _graphView.nodes)
            {
                _graphView.RemoveElement(node);
            }

            // Clear out all current connections.
            foreach (Edge connection in _graphView.edges)
            {
                _graphView.RemoveElement(connection);
            }

            // Create nodes from the save data.
            LoadNodes();

            // Create connections from the save data.
            foreach (NodeConnectionData connectionData in _data.Connections)
            {
                LoadConnection(connectionData);
            }
        }

        /// <summary>
        /// Creates nodes from the save data.
        /// Can be overriden to support loading different types.
        /// </summary>
        protected virtual void LoadNodes()
        {
            // Create nodes from the save data.
            foreach (BaseNodeData nodeData in _data.Nodes)
            {
                _graphView.AddNode(nodeData);
            }
        }

        /// <summary>
        /// Adds a connection between certain ports.
        /// Can be overriden for additional behavior.
        /// </summary>
        /// <param name="connectionData">The data to load from.</param>
        protected virtual void LoadConnection(NodeConnectionData connectionData)
        {
            // Get the input and output nodes based on the Guid in the data.
            BaseNode inputNode = (BaseNode)_graphView.nodes.Where(
                    (Node node) => ((BaseNode)node).Guid == connectionData.InputNodeGuid
                ).First();
            BaseNode outputNode = (BaseNode)_graphView.nodes.Where(
                (Node node) => ((BaseNode)node).Guid == connectionData.OutputNodeGuid
            ).First();

            // Get the input and output ports based on the indexes in the data.
            Port inputPort = (Port)inputNode.inputContainer[connectionData.InputPortIndex];
            Port outputPort = (Port)outputNode.outputContainer[connectionData.OutputPortIndex];

            // Create and add the connection to the GraphView.
            Edge edge = inputPort.ConnectTo(outputPort);
            _graphView.AddElement(edge);
        }

        /// <summary>
        /// Saves the editor to the data.
        /// </summary>
        protected virtual void Save()
        {
            // Clear the current save data.
            ClearData();

            // Save the nodes.
            foreach (Node node in _graphView.nodes)
            {
                SaveNode(node);
            }

            // Save the connections.
            foreach (Edge connection in _graphView.edges)
            {
                SaveConnection(connection);
            }

            // Save the data to the asset.
            _serializedEditorData.ApplyModifiedProperties();
        }

        /// <summary>
        /// Clears the save data before saving.
        /// Can be overriden for additional behavior.
        /// </summary>
        protected virtual void ClearData()
        {
            _serializedNodes.ClearArray();
            _serializedConnections.ClearArray();
        }

        /// <summary>
        /// Saves a node.
        /// Can be overriden for additional behavior.
        /// </summary>
        /// <param name="node">The node to save.</param>
        protected virtual void SaveNode(Node node)
        {
            if (node is BaseNode)
            {
                // Create the base node data.
                BaseNode baseNode = (BaseNode)node;
                BaseNodeData baseNodeData = new BaseNodeData
                {
                    Guid = baseNode.Guid,
                    Position = baseNode.GetPosition().position
                };

                // Add the base node data to the save data.
                SerializedProperty serializedNode = InsertElementIntoSerializedArray(_serializedNodes);
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Guid)).stringValue = baseNodeData.Guid;
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Position)).vector2Value = baseNodeData.Position;
            }
        }

        /// <summary>
        /// Saves a connection.
        /// Can be overriden for additional behavior.
        /// </summary>
        /// <param name="connection"></param>
        protected virtual void SaveConnection(Edge connection)
        {
            BaseNode inputNode = (BaseNode)connection.input.node;
            BaseNode outputNode = (BaseNode)connection.output.node;
            NodeConnectionData connectionData = new NodeConnectionData
            {
                InputNodeGuid = inputNode.Guid,
                InputPortIndex = inputNode.inputContainer.IndexOf(connection.input),
                OutputNodeGuid = outputNode.Guid,
                OutputPortIndex = outputNode.outputContainer.IndexOf(connection.output),
            };

            SerializedProperty serializedConnection = InsertElementIntoSerializedArray(_serializedConnections);
            serializedConnection.FindPropertyRelative(
                nameof(NodeConnectionData.InputNodeGuid)
            ).stringValue = connectionData.InputNodeGuid;
            serializedConnection.FindPropertyRelative(
                nameof(NodeConnectionData.InputPortIndex)
            ).intValue = connectionData.InputPortIndex;
            serializedConnection.FindPropertyRelative(
                nameof(NodeConnectionData.OutputNodeGuid)
            ).stringValue = connectionData.OutputNodeGuid;
            serializedConnection.FindPropertyRelative(
                nameof(NodeConnectionData.OutputPortIndex)
            ).intValue = connectionData.OutputPortIndex;
        }

        /// <summary>
        /// Inserts an element into an array and returns the element.
        /// </summary>
        /// <param name="array">The serialized property array.</param>
        /// <returns>The serialized property array element.</returns>
        protected SerializedProperty InsertElementIntoSerializedArray(SerializedProperty array)
        {
            array.InsertArrayElementAtIndex(array.arraySize);
            return array.GetArrayElementAtIndex(array.arraySize - 1);
        }
    }
}
