using System;
using Florenia.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Florenia.Characters.Player
{
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        #region Health

        public Health Health;
        public Healthbar healthbar;

        #endregion
        
        public int Damage = 20;

        private Inventory.Inventory _inventory;
        private Animator anim;

        private void Awake()
        {
            Health = GetComponent<Health>();
        }

        private void Start()
        {
            healthbar = FindObjectOfType<Healthbar>();
            Health.ResetToFull();
            healthbar.SetMaxHealth(Health.HealthPoints);
            anim = GetComponent<Animator>();
            _inventory = GetComponent<Inventory.Inventory>();
        }

        private void OnEnable()
        {
            Health.OnDeath += Die;
            Health.OnTakeDamage += SetUIHealth;
            Health.OnRegenerate += SetUIHealth;
        }
        private void OnDisable()
        {
            Health.OnDeath -= Die;
            Health.OnTakeDamage -= SetUIHealth;
            Health.OnRegenerate -= SetUIHealth;
        }

        private void SetUIHealth(int currentHealth)
        {
            healthbar.SetHealth(currentHealth);
        }

        public void Die()
        {
            Die(0);
        }
        
        public void Die(int death){
            anim.SetTrigger("Death");
            GameManager.Instance.AddDeath(death);
            Health.ResetToFull();
            SetUIHealth(Health.HealthPoints);
        }

        public void ResetInventory()
        {
            _inventory.ClearInventory();
        }
    }
}
