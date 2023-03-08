using UnityEngine;

namespace Florenia.Characters.Player
{
    public class HealthOld : MonoBehaviour
    {

        // Fields
        int _currentHealth;
        int _currentMaxHealth;

        // Properties
        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            set
            {
                _currentHealth = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return _currentMaxHealth;
            }
            set
            {
                _currentMaxHealth = value;
            }
        }

        // Constructor
        public HealthOld(int health, int maxHealth)
        {
            _currentHealth = health;
            _currentMaxHealth = maxHealth;
        }

        // Methods
        public void DamageUnit(int damageAmount)
        {
            if (_currentHealth > 0)
            {
                _currentHealth -= damageAmount;
            }
        }

        public void HealthUnit(int healAmount)
        {
            if (_currentHealth < _currentMaxHealth)
            {
                _currentHealth += healAmount;
            }
            if (_currentHealth > _currentMaxHealth)
            {
                _currentHealth = _currentMaxHealth;
            }
        }
    }
}