using System.Collections.Generic;
using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public int InventorySlots = 4;
        public List<InventorySlot> slots = new();
        
        private InventoryUI inventoryUI;

        private void Awake()
        {
            // inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
            AddEmptySlots(InventorySlots);
        }

        private void AddEmptySlots(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject iSlotGO = new GameObject($"Inventoryslot0{i}", typeof(InventorySlot));
                iSlotGO.transform.SetParent(transform);
                slots.Add(iSlotGO.GetComponent<InventorySlot>());
            }
        }
        
        [ContextMenu("Print slots")]
        private void DebugSlots()
        {
            foreach (InventorySlot slot in slots)
            {
                Debug.Log($"Found slot {slot}");
            }
        }
        
        /// <summary>
        /// Adds an item to a free slot
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool Add(InventoryItem item)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsEmpty())
                {
                    // Found a free slot
                    slot.item = item;

                    // Update the UI
                    // if (inventoryUI != null)
                    // {
                    //     inventoryUI.UpdateUI(this);
                    // }
                    return true;
                }
            }
            return false;
        }

        public bool ContainsItem(string itemId)
        {
            if (slots == null || slots.Count == 0)
                 return false;

            foreach (InventorySlot slot in slots)
            {
                if (!slot.IsEmpty())
                {
                    if (slot.item.itemId.Equals(itemId) || slot.item.itemId == itemId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}