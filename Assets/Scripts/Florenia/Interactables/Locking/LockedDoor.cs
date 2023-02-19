using DungeonArchitect.Flow.Items;
using Florenia.Characters.Player.Inventory;
using Florenia.Managers;
using UnityEngine;

namespace Florenia.Interactables.Locking
{
    public class LockedDoor : MonoBehaviour
    {
        private Animator animator;

        private string lockId;
        public string[] validKeys = new string[0];
 

        private void Start()
        {
            // find the door id (grab it from the item metadata component that DA creates)
            var lockItemMetadata = FindItemMetadata();
            if (lockItemMetadata != null)
            {
                lockId = lockItemMetadata.itemId;
                validKeys = lockItemMetadata.referencedItemIds;
            }

            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Search the current game object and works its way up the hierarchy to find the item metadata object 
        /// </summary>
        /// <returns></returns>
        FlowItemMetadataComponent FindItemMetadata()
        {
            var obj = gameObject;
            while (obj != null)
            {
                var itemMetadata = obj.GetComponent<FlowItemMetadataComponent>();
                if (itemMetadata != null)
                {
                    return itemMetadata;
                }

                var parentTransform = obj.transform.parent; 
                obj = (parentTransform != null) ? parentTransform.gameObject : null;
            }

            Debug.Log("No metadata found :(");
            return null;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && CanOpenDoor(other))
            {
                OpenDoor();
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CloseDoor();
            }
        }

        bool CanOpenDoor(Collider2D other)
        {
            Debug.Log("Trying to open door");
            var inventory = PlayerManager.Instance.Player.GetComponentInChildren<Inventory>();//other.gameObject.GetComponentInChildren<Inventory>();
            if (inventory != null)
            {
                // Check if any of the valid keys are present in the inventory of the collided object
                foreach (var validKey in validKeys)
                {
                    if (inventory.ContainsItem(validKey))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        void OpenDoor()
        {
            Debug.Log("Opening door");
            // animator.SetBool("doorOpen", true);
        }

        void CloseDoor()
        {
            Debug.Log("Closing door");
            // animator.SetBool("doorOpen", false);
        }

    }
}