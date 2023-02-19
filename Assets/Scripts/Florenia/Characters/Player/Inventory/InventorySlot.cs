using System;
using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public InventoryItem item = new InventoryItem();

        private void Awake()
        {
            Debug.Log("Initializing item!");
            item = new InventoryItem();
            Debug.Log($"Initialized as {item.itemType}");
        }
    }
}