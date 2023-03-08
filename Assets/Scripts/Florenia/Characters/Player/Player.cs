using Florenia.Managers;
using UnityEngine;

namespace Florenia.Characters.Player
{
    public class Player : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;
        public Healthbar healthbar;
        public int damage = 20;
        public bool GodMode = true;

        private Inventory.Inventory _inventory;
        private Animator anim;

        public bool IsDead
        {
            get
            {
                return currentHealth == 0;
            }
        }

        private void Start()
        {
            currentHealth = maxHealth;
            healthbar = FindObjectOfType<Healthbar>();
            healthbar.SetMaxHealth(maxHealth);
            anim = GetComponent<Animator>();
            _inventory = GetComponent<Inventory.Inventory>();
        }

        private void Update()
        {
        }

        public void TakeDamage(int damage)
        {
            if (GodMode)
                return;

            currentHealth -= damage;
            if (currentHealth < 20)
                currentHealth = 20;

            UpdateHealthBar();
            
            if(currentHealth <= 20)
            {
                anim.SetTrigger("Death");

                Die(0);
            }
        }

        public void RegenerateHealth(int hp)
        {
            int cHP = currentHealth + hp;
            currentHealth = cHP > maxHealth ? maxHealth : cHP;
            
            UpdateHealthBar();
        }

        private void UpdateHealthBar()
        {
            healthbar.SetHealth(currentHealth);

        }
        
        public void Die(int death){
            GameManager.Instance.AddDeath(death);
            currentHealth = maxHealth;
            UpdateHealthBar();
        }

        public void ResetInventory()
        {
            _inventory.ClearInventory();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                TakeDamage(damage);
            }
        }

    }
}
