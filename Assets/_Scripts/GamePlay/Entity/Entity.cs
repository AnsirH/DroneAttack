using System;
using _Scripts.Core.Interfaces;
using _Scripts.GamePlay.ScriptableObjects;
using UnityEngine;

namespace _Scripts.GamePlay.Entity
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityStatData _statData;
        
        protected IMovable _movement;
        protected IDamageable _health;
        [SerializeField] protected Animator _anim;
        
        protected virtual void Awake()
        {
            _movement = GetComponent<IMovable>();
            _movement?.SetMoveSpeed(_statData.moveSpeed);
            _health = GetComponent<IDamageable>();
            _health.SetMaxHealth(_statData.maxHealth);
            _health.SetHealth(_statData.maxHealth);
        }
        public abstract void Hit(float damage);
        public abstract void Die();
    }
}
