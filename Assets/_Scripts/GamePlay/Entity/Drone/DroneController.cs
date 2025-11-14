using System;
using System.Collections.Generic;
using _Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.GamePlay.Entity.Drone
{
    public enum EDroneState
    {
        MoveToTarget,
        DashAttack,
        Spawn,
        Destroy
    }
    
    public class DroneController : Entity
    {
        private static readonly HashSet<string> ValidTags = new() { "Player", "Entity", "Ground", "Obstacle" };
        private DroneState[] _droneStates = new DroneState[4];
        private StateMachine _stateMachine;
        private Vector3 _startPosition;
        private float _distanceLimit;
        
        public IMovable Movement => _movement;
        public Transform Target { get; private set; }
        public IObjectPool<DroneController> Pool { get; set; }

        public float DashSpeedRatio { get; private set; } = 1.5f;
        public float AttackRange { get; private set; } = 1.0f;
        public bool IsReleased { get; private set; }
        public ParticleSystem explosionParticle;
        public GameObject model;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new StateMachine(this);
            _droneStates[(int)EDroneState.MoveToTarget] = new DroneMoveState(this);
            _droneStates[(int)EDroneState.DashAttack] = new DroneAttackState(this);
            _droneStates[(int)EDroneState.Spawn] = new DroneSpawnState(this);
            _droneStates[(int)EDroneState.Destroy] = new DroneDestroyState(this);
        }

        private void OnEnable()
        {
            ChangeState(EDroneState.Spawn);
        }

        private void OnDisable()
        {
            ResetDrone();
        }

        private void ResetDrone()
        {
            _health.SetHealth(_statData.maxHealth);
            transform.rotation = Quaternion.identity;
            Target = null;
            model.SetActive(true);
        }

        public override void Hit(float damage)
        {
            if (_health.IsDead) return;
            _health.TakeDamage(damage);

            if (_health.IsDead)
                Die();
        }

        public override void Die()
        {
            ChangeState(EDroneState.Destroy);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Updated();
            
            if (Vector3.Distance(transform.position, _startPosition) > _distanceLimit)
                Hit(_statData.maxHealth);
        }

        public void SetTarget(Transform target)
        {
            Target = target;
        }

        public void SetDashSpeedRatio(float ratio)
        {
            DashSpeedRatio = ratio;
        }

        public void SetAttackRange(float range)
        {
            AttackRange = range;
        }

        public void SetIsReleased(bool isReleased)
        {
            IsReleased = isReleased;
        }

        public void Initialize(Vector3 startPosition, float distanceLimit, float dashSpeedRatio = 3.0f, float attackRange = 5.0f)
        {
            _startPosition = startPosition;
            _distanceLimit = distanceLimit;
            DashSpeedRatio = dashSpeedRatio;
            AttackRange = attackRange;
        }

        public void ChangeState(EDroneState state)
        {
            _stateMachine.Transition(_droneStates[(int)state]);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (ValidTags.Contains(other.tag))
            {
                if (other.gameObject.CompareTag("Player"))
                    other.GetComponentInParent<Entity>().Hit(_statData.attackPower);
                Hit(_statData.maxHealth);
            }
        }
    }
}
