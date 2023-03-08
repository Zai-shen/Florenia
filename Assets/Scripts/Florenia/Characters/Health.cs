using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
namespace Florenia.Characters
{
    public class Health : MonoBehaviour
    {
        public Action OnDeath;
        public Action<int> OnTakeDamage;
        public Action<int> OnRegenerate;
        public bool GodMode;
        public int HealthPoints = 100;
        private int _startingHP;

        private bool _tookDamageRecently;
        public float _invulnerabilityCooldown;
    
        private void Awake()
        {
            _startingHP = HealthPoints;
        }
    
        public void ResetToFull()
        {
            HealthPoints = _startingHP;
        }

        public void RegenerateHealth(int hp)
        {
            int cHP = HealthPoints + hp;
            HealthPoints = cHP > _startingHP ? _startingHP : cHP;
            OnRegenerate?.Invoke(HealthPoints);
        }

        public void TryTakeDamage(int dmg)
        {
            if (!enabled) return;
            if (_tookDamageRecently) return;
        
            TakeDamage(dmg);
            StartCoroutine(MakeInvulnerable(_invulnerabilityCooldown));
        }

        private IEnumerator MakeInvulnerable(float duration)
        {
            _tookDamageRecently = true;
            yield return new WaitForSeconds(duration);
            _tookDamageRecently = false;
            yield return null;
        }
    
        public void TakeDamage(int dmg)
        {
            Debug.Log($"Taking {dmg} damage");
            HealthPoints -= dmg;
            OnTakeDamage?.Invoke(HealthPoints);
        
            if (HealthPoints <= 0)
            {
                Die();
            }
        }
    
        public void Die()
        {
            if (GodMode)
                return;
        
            OnDeath?.Invoke();
        }
    }

}