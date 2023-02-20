using UnityEngine;

namespace Florenia.Characters.Player.Inventory
{
    [System.Serializable]
    public class InventoryItem
    {
        public InventoryItemType itemType = InventoryItemType.None;
        public string itemId ="";
        public Sprite icon = null;
        
        [ContextMenu("Print item")]
        private void DoPrint()
        {
            Debug.Log(this);
        }
        
        public override string ToString()
        {
            return base.ToString() + $"Inventory item: itemtype = {itemType}, itemid={itemId}, icon={icon}";
        }
    }
}