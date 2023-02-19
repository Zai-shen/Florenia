using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    [System.Serializable]
    public class InventoryItem
    {
        public InventoryItemType itemType = InventoryItemType.None;
        public string itemId;
        public Sprite icon = null;
    }
}