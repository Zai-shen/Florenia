using DungeonArchitect.Flow.Items;
using Florenia.Characters.Player.Inventory;
using Florenia.Managers;
using UnityEngine;

namespace Florenia.Interactables.Locking
{
    public class LockedDoor : MonoBehaviour
    {
        private string lockId;
        public string[] validKeys = new string[0];

        private bool locked = true;
        public Collider2D LockCollider;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            // find the door id (grab it from the item metadata component that DA creates)
            FlowItemMetadataComponent lockItemMetadata = FindItemMetadata();
            if (lockItemMetadata != null)
            {
                lockId = lockItemMetadata.itemId;
                validKeys = lockItemMetadata.referencedItemIds;
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// Search the current game object and works its way up the hierarchy to find the item metadata object 
        /// </summary>
        /// <returns></returns>
        private FlowItemMetadataComponent FindItemMetadata()
        {
            GameObject obj = transform.gameObject;
            while (obj != null)
            {
                FlowItemMetadataComponent itemMetadata = obj.GetComponent<FlowItemMetadataComponent>();
                if (itemMetadata != null)
                {
                    return itemMetadata;
                }

                Transform parentTransform = obj.transform.parent; 
                obj = (parentTransform != null) ? parentTransform.gameObject : null;
            }

            Debug.Log("No metadata found :(");
            return null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (locked && other.CompareTag("Player") && CanOpenDoor(other))
            {
                OpenDoor();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!locked && other.CompareTag("Player"))
            {
                CloseDoor();
            }
        }

        private bool CanOpenDoor(Collider2D other)
        {
            Inventory inventory = PlayerManager.Instance.InGamePlayer.GetComponentInChildren<Inventory>();//other.gameObject.GetComponentInChildren<Inventory>();
            if (inventory != null)
            {
                if (HasValidKey(inventory)) return true;
            }
            return false;
        }

        private bool HasValidKey(Inventory inventory)
        {
            foreach (string validKey in validKeys)
            {
                if (inventory.ContainsItem(validKey))
                {
                    return true;
                }
            }

            return false;
        }

        private void OpenDoor()
        {
            Unlock();
        }

        private void CloseDoor()
        {
            Lock();
        }

        public void Lock()
        {
            LockCollider.isTrigger = false;
            locked = true;
            
            ChangeSpriteTransparency(1f);
        }

        public void Unlock()
        {
            LockCollider.isTrigger = true;
            locked = false;
            
            ChangeSpriteTransparency(0.2f);
        }

        private void ChangeSpriteTransparency(float alpha)
        {
            Color initialColor = _spriteRenderer.color;
            initialColor.a = alpha;
            _spriteRenderer.color = initialColor;
        }
    }
}