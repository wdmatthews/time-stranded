using UnityEngine;
using UnityEditor;

namespace TimeStranded.Inventory.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="InventorySO"/>s in the inspector.
    /// </summary>
    [CustomEditor(typeof(InventorySO))]
    public class InventorySOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The item to use.
        /// </summary>
        private ItemSO _item = null;

        /// <summary>
        /// The amount to change the item amount by.
        /// </summary>
        private int _amount = 1;

        public override void OnInspectorGUI()
        {
            InventorySO inventory = (InventorySO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _item = (ItemSO)EditorGUILayout.ObjectField("Item", _item, typeof(ItemSO), false);
            _amount = EditorGUILayout.IntField("Item Amount", _amount);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add")) inventory.AddItem(_item, _amount);
            if (GUILayout.Button("Remove")) inventory.RemoveItem(_item, _amount);
            if (GUILayout.Button("Count")) Debug.Log(inventory.GetItemCount(_item));
            GUILayout.EndHorizontal();
        }
    }
}
