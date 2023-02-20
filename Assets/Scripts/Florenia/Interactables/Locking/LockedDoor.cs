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

        private void Start()
        {
            // find the door id (grab it from the item metadata component that DA creates)
            FlowItemMetadataComponent lockItemMetadata = FindItemMetadata();
            if (lockItemMetadata != null)
            {
                lockId = lockItemMetadata.itemId;
                validKeys = lockItemMetadata.referencedItemIds;
            }
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
            Debug.Log("Can open door?");
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
            Debug.Log("Opening door");
        }

        private void CloseDoor()
        {
            Lock();
            Debug.Log("Closing door");
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }
    }
}