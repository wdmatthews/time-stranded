using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using Toolkits.NodeEditor.Editor;

namespace Toolkits.NodeEditor.Test.Editor
{
    /// <summary>
    /// A test node editor.
    /// </summary>
    public class TestNodeEditorWindow : BaseNodeEditorWindow
    {
        /// <summary>
        /// The name of the editor window.
        /// </summary>
        protected override string Name => "Test Node Editor";

        /// <summary>
        /// The data used to store information used by the test node editor.
        /// </summary>
        protected TestEditorDataSO Data { get; set; } = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save test nodes.
        /// </summary>
        protected SerializedProperty _serializedTestNodes = null;

        /// <summary>
        /// The graph view to use in this node editor window.
        /// </summary>
        protected TestGraphView _testGraphView = null;

        /// <summary>
        /// The toolbar's add test node button.
        /// </summary>
        protected ToolbarButton _toolbarAddTestNodeButton = null;

        /// <summary>
        /// Called when an asset is opened to try opening the node editor.
        /// </summary>
        /// <param name="instanceId">The asset's instance ID.</param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(1)]
        public static bool ShowWindow(int instanceId, int line)
        {
            // Get the selected asset and check if it is the correct type.
            Object asset = EditorUtility.InstanceIDToObject(instanceId);
            if (!(asset is TestEditorDataSO)) return false;

            // Open the node editor window and initialize its values.
            TestNodeEditorWindow window = GetWindow<TestNodeEditorWindow>();
            window.minSize = window.MinSize;
            window._data = (BaseEditorDataSO)asset;
            window.Data = (TestEditorDataSO)asset;
            // Load the data.
            window.titleContent.text = window.Name;
            window.Load();

            return false;
        }

        /// <summary>
        /// Returns a new <see cref="TestGraphView"/> with a style sheet.
        /// </summary>
        /// <returns>The new <see cref="TestGraphView"/>.</returns>
        protected override BaseGraphView CreateGraphView()
        {
            _testGraphView = new TestGraphView(_styleSheet);
            return _testGraphView;
        }

        /// <summary>
        /// Adds to the toolbar at the top of the node editor.
        /// </summary>
        protected override void CreateToolbar()
        {
            // Create the toolbar.
            base.CreateToolbar();

            // Add a button to the toolbar.
            _toolbarAddTestNodeButton = new ToolbarButton(() => _testGraphView.AddTestNode(new Vector2()));
            _toolbarAddTestNodeButton.text = "Add Test Node";
            _toolbar.Add(_toolbarAddTestNodeButton);
        }

        /// <summary>
        /// Loads the node editor from the data.
        /// </summary>
        protected override void Load()
        {
            base.Load();
            _serializedTestNodes = _serializedEditorData.FindProperty(nameof(TestEditorDataSO.TestNodes));
        }

        /// <summary>
        /// Creates nodes from the save data.
        /// </summary>
        protected override void LoadNodes()
        {
            foreach (TestNodeData nodeData in Data.TestNodes)
            {
                _testGraphView.AddTestNode(nodeData);
            }
        }

        /// <summary>
        /// Clears the test nodes before saving.
        /// </summary>
        protected override void ClearData()
        {
            base.ClearData();
            _serializedTestNodes.ClearArray();
        }

        /// <summary>
        /// Saves a node.
        /// </summary>
        /// <param name="node">The node to save.</param>
        protected override void SaveNode(Node node)
        {
            if (node is TestNode)
            {
                // Create the test node data.
                TestNode testNode = (TestNode)node;
                TestNodeData testNodeData = new TestNodeData
                {
                    Guid = testNode.Guid,
                    Position = testNode.GetPosition().position,
                    TestString = testNode.Data.TestString
                };

                // Add the test node data to the save data.
                SerializedProperty serializedNode = InsertElementIntoSerializedArray(_serializedNodes);
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Guid)).stringValue = testNodeData.Guid;
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Position)).vector2Value = testNodeData.Position;
                SerializedProperty serializedTestNode = InsertElementIntoSerializedArray(_serializedTestNodes);
                serializedTestNode.FindPropertyRelative(nameof(TestNodeData.Guid)).stringValue = testNodeData.Guid;
                serializedTestNode.FindPropertyRelative(nameof(TestNodeData.Position)).vector2Value = testNodeData.Position;
                serializedTestNode.FindPropertyRelative(nameof(TestNodeData.TestString)).stringValue = testNodeData.TestString;
            }
        }
    }
}
