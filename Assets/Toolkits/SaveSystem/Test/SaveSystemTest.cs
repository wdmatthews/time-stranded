using UnityEngine;

namespace Toolkits.SaveSystem.Test
{
    /// <summary>
    /// Used to test the save system.
    /// </summary>
    [AddComponentMenu("Toolkits/Save System/Test/Test")]
    [DisallowMultipleComponent]
    public class SaveSystemTest : MonoBehaviour
    {
        /// <summary>
        /// The test save manager.
        /// </summary>
        [Tooltip("The test save manager.")]
        [SerializeField] private TestSaveManagerSO _saveManager = null;

        private void Start()
        {
            // Save test data using both methods.
            _saveManager.Save("TestSave1", new TestSaveData {
                StringData = "1",
                IntData = 1,
                Vector2Data = new Vector2(1, 1)
            });
            Debug.Log("Data saved under TestSave1.");

            _saveManager.SaveData = new TestSaveData
            {
                StringData = "2",
                IntData = 2,
                Vector2Data = new Vector2(2, 2)
            };
            _saveManager.Save("TestSave2");
            Debug.Log("Data saved under TestSave2.");

            // Load test data using both methods.
            TestSaveData save1 = _saveManager.Load("TestSave1");
            Debug.Log($"Data loaded from TestSave1: {save1.StringData} {save1.IntData} {save1.Vector2Data}");

            _saveManager.Load("TestSave2");
            TestSaveData save2 = _saveManager.SaveData;
            Debug.Log($"Data loaded from TestSave2: {save2.StringData} {save2.IntData} {save2.Vector2Data}");

            // Delete test data.
            _saveManager.DeleteSave("TestSave1");
            _saveManager.DeleteSave("TestSave2");
            Debug.Log($"TestSave1 deleted: {_saveManager.Load("TestSave1") == null}");
            Debug.Log($"TestSave2 deleted: {_saveManager.Load("TestSave2") == null}");
        }
    }
}
