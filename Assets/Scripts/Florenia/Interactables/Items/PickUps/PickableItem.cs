using System.Collections;
using DungeonArchitect.Flow.Items;
using Florenia.Characters.Player.Inventory;
using Florenia.Managers;
using UnityEngine;

namespace Florenia.Interactables.Items.PickUps
{
    [RequireComponent(typeof(PickUpEffect))]
    public class PickableItem : MonoBehaviour
    {
        public PickUp PickUpSO;
        public InventoryItemType itemType;
        public Sprite icon;

        private bool _ableToCollide = true;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_ableToCollide) return;
            if (!other.CompareTag("Player")) return;

            ApplyTo(other);
            Destroy(gameObject);
        }

        private void ApplyTo(Collider2D other)
        {
            PickUpSO.Apply(other.gameObject);
            if (PickUpSO.AddToInventory)
            {
                AddToInventory();
            }
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

        private string GetItemId()
        {
            var itemMetadata = GetComponent<FlowItemMetadataComponent>();
            return (itemMetadata != null) ? itemMetadata.itemId : "";
        }

        public void SetLateCollidable(float duration)
        {
            StartCoroutine(SetCollidable(duration));
        }

        private IEnumerator SetCollidable(float duration)
        {
            _ableToCollide = false;
            yield return new WaitForSeconds(duration);
            _ableToCollide = true;
        }
    }
}