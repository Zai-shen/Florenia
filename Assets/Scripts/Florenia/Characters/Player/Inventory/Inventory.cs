using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    public class Inventory : MonoBehaviour
    {
        public int InventorySlots = 4;
        public InventorySlot[] slots;
        InventoryUI inventoryUI;

        private void Awake()
        {
            // inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
            CreateEmptySlots(InventorySlots);
        }

        private void CreateEmptySlots(int count)
        {
            // slots = new InventorySlot[count];
            
            // for (int i = 0; i < slots.Length; i++)
            // {
            //     GameObject iSlotGO = new GameObject($"Inventoryslot0{i}");
            //     InventorySlot iSlot = iSlotGO.AddComponent<InventorySlot>();
            //     iSlot.transform.parent = transform;
            //     slots[i] = iSlot;//.GetComponent<InventorySlot>();
            //     slots[i].item = new InventoryItem();
            //     slots[i].item.itemType = InventoryItemType.None;
            // }
        }
        
        /// <summary>
        /// Adds an item to a free slot
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool Add(InventoryItem item)
        {
            foreach (var slot in slots)
            {
                if (slot.item.itemType == InventoryItemType.None)
                {
                    Debug.Log("Adding item with id " + item.itemId);
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
            // if (slots == null || slots.Length == 0)
            //     return false;

            foreach (InventorySlot slot in slots)
            {
                Debug.Log($"Contains item?: {itemId} ");
                if (slot.item.itemType != InventoryItemType.None)
                {
                    Debug.Log($"Testing: {slot.item.itemType}");
                    if (slot.item.itemId.Equals(itemId) || slot.item.itemId == itemId)
                    {
                        Debug.Log("Yes it cointains!");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}