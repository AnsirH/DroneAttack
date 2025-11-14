using _Scripts.Core.Interfaces;
using UnityEngine;

namespace _Scripts.GamePlay.Entity
{
    public class Health : MonoBehaviour, IDamageable
    {
        private float _currentHealth;
        private float _maxHealth;
        
        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;
        public bool IsDead => _currentHealth <= 0;
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            if (IsDead)
                _currentHealth = 0;
        }

        public void SetHealth(float health)
        {
            _currentHealth = Mathf.Clamp(health, 0, _maxHealth);
        }

        public void SetMaxHealth(float health)
        {
            _maxHealth = health;
        }
    }
}
