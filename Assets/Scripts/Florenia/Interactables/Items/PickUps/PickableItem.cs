using System.Collections.Generic;
using System.Linq;
using DungeonArchitect.Flow.Items;
using Florenia.Interactables.Locking;
using UnityEngine;
using Inventory = Florenia.Characters.Player.Inventory.Inventory;
using InventoryItem = Florenia.Characters.Player.Inventory.InventoryItem;
using InventoryItemType = Florenia.Characters.Player.Inventory.InventoryItemType;

namespace Florenia.Interactables.Items.PickUps
{
    public class PickableItem : MonoBehaviour
    {
        public InventoryItemType itemType;
        public Sprite icon = null;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            Debug.Log($"Picking up {transform.name}");
            var go = other.gameObject;
            var inventory = go.GetComponentInChildren<Inventory>();
            if (inventory != null)
            {
                var item = new InventoryItem();
                item.itemType = itemType;
                item.itemId = GetItemId();
                item.icon = icon;

                if (inventory.Add(item))
                {
                    // We successfully added this item to the inventory. destroy this game object
                    DestroyDoor(item.itemId);
                    
                    Destroy(gameObject);
                    return;
                }
            }
        }

        private void DestroyDoor(string theID)
        {
            LockedDoor[] _lockedDoors = FindObjectsOfType<LockedDoor>();
            foreach (LockedDoor door in _lockedDoors)
            {
                foreach (var id in door.validKeys)
                {
                    if (id.Equals(theID))
                    {
                        Destroy(door.gameObject);
                        return;
                    }
                }
            }
        }

        string GetItemId()
        {
            var itemMetadata = GetComponent<FlowItemMetadataComponent>();
            return (itemMetadata != null) ? itemMetadata.itemId : "";
        }
    }
}