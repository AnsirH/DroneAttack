using System;
using UnityEngine;

namespace _Scripts.GamePlay.Entity.Player
{
    // 가지고 있는 무기에 대한 사용
    // ik 적용
    public class PlayerAttacker : MonoBehaviour
    {
        public Transform weaponPivot;
        public Transform leftHandMount;
        public Transform rightHandMount;

        [SerializeField] private Animator _animator;


        private void OnAnimatorIK(int layerIndex)
        {
            Debug.Log("animator IK");
            if (_animator == null) return;
            weaponPivot.position = _animator.GetIKHintPosition(AvatarIKHint.RightElbow);
            
            // IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
            
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);
            
            // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
            _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
            
            _animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
        }
    }
}
