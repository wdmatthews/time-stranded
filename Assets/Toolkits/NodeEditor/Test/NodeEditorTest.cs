using UnityEngine;

namespace Toolkits.NodeEditor.Test
{
    /// <summary>
    /// Used to test the node editor data.
    /// </summary>
    [AddComponentMenu("Toolkits/Node Editor/Test/Test")]
    [DisallowMultipleComponent]
    public class NodeEditorTest : MonoBehaviour
    {
        /// <summary>
        /// The test editor data.
        /// </summary>
        [Tooltip("The test editor data.")]
        [SerializeField] private TestEditorDataSO _editorData = null;

        private void Start()
        {
            foreach (TestNodeData testNode in _editorData.TestNodes)
            {
                Debug.Log($"Test Node {testNode.TestString} with Guid: {testNode.Guid}");
            }

            foreach (NodeConnectionData connection in _editorData.Connections)
            {
                Debug.Log($"Connection from {connection.InputNodeGuid} port {connection.InputPortIndex} to {connection.OutputNodeGuid} port {connection.OutputPortIndex}.");
            }
        }
    }
}
