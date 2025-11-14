using System;
using _Scripts.Core.Interfaces;
using UnityEngine;

namespace _Scripts.GamePlay.Entity
{
    public class Movement : MonoBehaviour, IMovable
    {
        // Variables
        private float _moveSpeed = 0.0f;
        protected CharacterController _controller;
        public bool shouldRotateToTarget = false;

        public float MoveSpeed => _moveSpeed;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public virtual void Move(Vector3 direction)
        {
            if (_controller)
                _controller.Move(direction * (_moveSpeed * Time.deltaTime));
            else
                transform.position += direction * (_moveSpeed * Time.deltaTime);
            
            if (shouldRotateToTarget)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 720 * Time.deltaTime);
        }
    }
}
