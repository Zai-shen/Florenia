using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        healthbar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(damage);
        }
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
            SceneManager.LoadScene(1);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            
                TakeDamage(damage);
           
            
        }
    }

}
