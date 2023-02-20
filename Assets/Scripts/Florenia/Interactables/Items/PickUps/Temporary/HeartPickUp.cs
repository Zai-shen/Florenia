using System;
using Florenia.Managers;
using UnityEngine;

namespace Florenia.Items.PickUps.Temporary
{
    public class HeartPickUp : MonoBehaviour
    {
        public int Health = 10;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            
            PlayerManager.Instance.InGamePlayer.RegenerateHealth(Health);
            Destroy(transform.gameObject);
        }
    }
}
