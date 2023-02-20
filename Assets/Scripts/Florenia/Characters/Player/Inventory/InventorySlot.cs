using System;
using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public InventoryItem item = new();
        
        public bool IsEmpty()
        {
            return item.itemType == InventoryItemType.None;
        }
        
        [ContextMenu("Print slot")]
        private void DoPrint()
        {
            Debug.Log(this);
        }
        
        public override string ToString()
        {
            return base.ToString() + $"Inventory Slot: item={item}";
        }
    }
}