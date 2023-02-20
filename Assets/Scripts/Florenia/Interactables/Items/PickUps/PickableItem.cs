using DungeonArchitect.Flow.Items;
using Florenia.Characters.Player.Inventory;
using Florenia.Interactables.Locking;
using Florenia.Managers;
using UnityEngine;

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

            AddToInventory();
        }

        private void AddToInventory()
        {
            Inventory inventory = PlayerManager.Instance.InGamePlayer.GetComponentInChildren<Inventory>();
            if (inventory != null)
            {
                InventoryItem item = new()
                {
                    itemType = itemType,
                    itemId = GetItemId(),
                    icon = icon
                };

                if (!inventory.Add(item))
                {
                    Debug.Log($"Could not add item from {transform}");
                }
                else
                {
                    Destroy(transform.gameObject);
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