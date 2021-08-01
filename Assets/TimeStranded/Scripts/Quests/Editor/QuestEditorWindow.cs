using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using Toolkits.NodeEditor;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Quests.Editor
{
    /// <summary>
    /// A node editor for quests.
    /// </summary>
    public class QuestEditorWindow : BaseNodeEditorWindow
    {
        /// <summary>
        /// The name of the editor window.
        /// </summary>
        protected override string Name => "Quest Editor";

        /// <summary>
        /// The data used to store information used by the quest node editor.
        /// </summary>
        protected QuestEditorDataSO Data { get; set; } = null;

        /// <summary>
        /// The <see cref="SerializedProperty"/> used to save quest nodes.
        /// </summary>
        protected SerializedProperty _serializedQuestNodes = null;

        /// <summary>
        /// The graph view to use in this node editor window.
        /// </summary>
        protected QuestGraphView _questGraphView = null;

        /// <summary>
        /// The toolbar's add quest node button.
        /// </summary>
        protected ToolbarButton _toolbarAddQuestNodeButton = null;

        /// <summary>
        /// The quest node data by guid, used in saving connections.
        /// </summary>
        protected Dictionary<string, SerializedProperty> _questDataByGuid = null;

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
            if (!(asset is QuestEditorDataSO)) return false;

            QuestEditorWindow window = GetWindow<QuestEditorWindow>();
            window.minSize = window.MinSize;
            window._data = (BaseEditorDataSO)asset;
            window.Data = (QuestEditorDataSO)asset;
            window.titleContent.text = window.Name;
            window.Load();

            return false;
        }

        /// <summary>
        /// Returns a new <see cref="QuestGraphView"/> with a style sheet.
        /// </summary>
        /// <returns>The new <see cref="QuestGraphView"/>.</returns>
        protected override BaseGraphView CreateGraphView()
        {
            _questGraphView = new QuestGraphView(_styleSheet);
            return _questGraphView;
        }

        /// <summary>
        /// Adds to the toolbar at the top of the node editor.
        /// </summary>
        protected override void CreateToolbar()
        {
            base.CreateToolbar();

            _toolbarAddQuestNodeButton = new ToolbarButton(() => _questGraphView.AddQuestNode(new Vector2()));
            _toolbarAddQuestNodeButton.text = "Add Quest Node";
            _toolbar.Add(_toolbarAddQuestNodeButton);
        }

        /// <summary>
        /// Loads the node editor from the data.
        /// </summary>
        protected override void Load()
        {
            base.Load();
            _serializedQuestNodes = _serializedEditorData.FindProperty(nameof(QuestEditorDataSO.QuestNodes));
        }

        /// <summary>
        /// Creates nodes from the save data.
        /// </summary>
        protected override void LoadNodes()
        {
            foreach (QuestNodeData nodeData in Data.QuestNodes)
            {
                _questGraphView.AddQuestNode(nodeData);
            }
        }

        /// <summary>
        /// Clears the quest nodes before saving.
        /// </summary>
        protected override void ClearData()
        {
            base.ClearData();
            _serializedQuestNodes.ClearArray();
            _questDataByGuid = new Dictionary<string, SerializedProperty>();
        }

        /// <summary>
        /// Saves a node.
        /// </summary>
        /// <param name="node">The node to save.</param>
        protected override void SaveNode(Node node)
        {
            if (node is QuestNode)
            {
                QuestNode questNode = (QuestNode)node;
                QuestNodeData questNodeData = new QuestNodeData
                {
                    Guid = questNode.Guid,
                    Position = questNode.GetPosition().position,
                    Quest = questNode.Data.Quest
                };

                SerializedProperty serializedNode = InsertElementIntoSerializedArray(_serializedNodes);
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Guid)).stringValue = questNodeData.Guid;
                serializedNode.FindPropertyRelative(nameof(BaseNodeData.Position)).vector2Value = questNodeData.Position;
                SerializedProperty serializedQuestNode = InsertElementIntoSerializedArray(_serializedQuestNodes);
                serializedQuestNode.FindPropertyRelative(nameof(QuestNodeData.Guid)).stringValue = questNodeData.Guid;
                serializedQuestNode.FindPropertyRelative(nameof(QuestNodeData.Position)).vector2Value = questNodeData.Position;
                serializedQuestNode.FindPropertyRelative(nameof(QuestNodeData.Quest)).objectReferenceValue = questNodeData.Quest;
                serializedQuestNode.FindPropertyRelative(nameof(QuestNodeData.PrerequisiteIds)).ClearArray();
                serializedQuestNode.FindPropertyRelative(nameof(QuestNodeData.NextIds)).ClearArray();
                _questDataByGuid.Add(questNodeData.Guid, serializedQuestNode);
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

            if (inputNode is QuestNode && outputNode is QuestNode)
            {
                // Add the output as a next quest id to the input,
                // and add the input as a prerequisite quest id to the output.
                SerializedProperty inputQuestData = _questDataByGuid[inputNode.Guid];
                SerializedProperty outputQuestData = _questDataByGuid[outputNode.Guid];
                InsertElementIntoSerializedArray(
                    outputQuestData.FindPropertyRelative(nameof(QuestNodeData.PrerequisiteIds))
                ).stringValue = inputNode.Guid;
                InsertElementIntoSerializedArray(
                    inputQuestData.FindPropertyRelative(nameof(QuestNodeData.NextIds))
                ).stringValue = outputNode.Guid;
            }
        }
    }
}
