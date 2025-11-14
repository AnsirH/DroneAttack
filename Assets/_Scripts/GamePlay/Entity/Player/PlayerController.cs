using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.GamePlay.Entity.Player
{
    public class PlayerController : Entity
    {
        private readonly int _rollID = Animator.StringToHash("Roll");
        private readonly int _moveID = Animator.StringToHash("Move");
        private readonly int _directionXid = Animator.StringToHash("DirectionX");
        private readonly int _directionZid = Animator.StringToHash("DirectionZ");
        private readonly int _dieID = Animator.StringToHash("Die");
        private Vector3 _inputVector;
        private bool _rollingTrigger;
        private PlayerMovement _playerMovement;
        public Camera targetCamera;

        protected override void Awake()
        {
            base.Awake();
            _playerMovement = _movement as PlayerMovement;
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
            _anim.SetTrigger(_dieID);
        }

        private void Update()
        {
            if (_health.CurrentHealth <= 0) return;
            
            if (!_playerMovement.IsRolling && _inputVector.sqrMagnitude > 0.01f)
            {
                Vector3 direction = _inputVector.normalized;
                if (targetCamera)
                    direction = GetCameraRelativeDirection(_inputVector);
                
                _movement.Move(direction);
                _anim.SetBool(_moveID, true);
                _anim.SetFloat(_directionXid, _inputVector.x);
                _anim.SetFloat(_directionZid, _inputVector.z);
            }
            else
            {
                _anim.SetBool(_moveID, false);
            }
            
            if (!_playerMovement.IsRolling && _rollingTrigger)
            {
                if (_inputVector.sqrMagnitude > 0.01f)
                    _playerMovement.Roll(GetCameraRelativeDirection(_inputVector));
                else
                    _playerMovement.Roll(transform.forward);
                _anim.SetTrigger(_rollID);
                _rollingTrigger = false;
            }
        }

        private void LateUpdate()
        {
            if (_health.CurrentHealth <= 0) return;
            if (targetCamera && !_playerMovement.IsRolling)
            {
                Vector3 cameraForward = targetCamera.transform.forward;
                cameraForward.y = 0;
                _playerMovement.Rotate(cameraForward);
            }
        }

        public void OnMove(InputValue value)
        {
            _inputVector.x = value.Get<Vector2>().x;
            _inputVector.z = value.Get<Vector2>().y;
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed && !_playerMovement.IsRolling)
            {
                _rollingTrigger = true;
            }
        }

        private Vector3 GetCameraRelativeDirection(Vector3 direction)
        {
            Vector3 cameraForward = targetCamera.transform.forward;
            Vector3 cameraRight = targetCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            return (cameraForward * _inputVector.z + cameraRight * _inputVector.x).normalized;
        }
    }
}
