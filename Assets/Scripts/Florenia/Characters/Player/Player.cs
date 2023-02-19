using Florenia.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Florenia.Characters.Player
{
    public class Player : MonoBehaviour
    {
        public int maxHealth = 100;
        public int currentHealth;
        public Healthbar healthbar;
        public int damage = 20;
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
        }

        private void Update()
        {
        }

        void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 20)
                currentHealth = 20;

            healthbar.SetHealth(currentHealth);

            if(currentHealth <= 20)
            {
                anim.SetTrigger("Death");

                Die(0);
            }
        }
        
        public void Die(int death){
            GameManager.Instance.AddDeath(death);
            currentHealth = maxHealth;
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
