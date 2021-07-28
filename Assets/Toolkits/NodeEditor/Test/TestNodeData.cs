namespace Toolkits.NodeEditor.Test
{
    /// <summary>
    /// Stores the data needed to create a test node.
    /// </summary>
    [System.Serializable]
    public class TestNodeData : BaseNodeData
    {
        /// <summary>
        /// A test string to edit and save.
        /// </summary>
        public string TestString = "Test Node";

        /// <summary>
        /// Creates empty node data.
        /// </summary>
        public TestNodeData() { }

        /// <summary>
        /// Creates node data from the given data.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public TestNodeData(TestNodeData data)
            : base(data)
        {
            TestString = data.TestString;
        }
    }
}
