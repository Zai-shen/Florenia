using System;
using UnityEngine;

namespace Florenia.Items.PickUps.Temporary
{
    public class HeartPickUp : MonoBehaviour
    {
        public float Health;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Debug.Log($"Picked Up Health + {Health}");
            }
        }
    }
}
