using UnityEngine;

namespace _Scripts.Core.Interfaces
{
    public interface IMovable
    {
        public float MoveSpeed { get; }
        public void SetMoveSpeed(float moveSpeed);
        public void Move(Vector3 direction);
    }
}
