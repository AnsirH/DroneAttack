using System.Collections;
using UnityEngine;

namespace _Scripts.GamePlay.Entity.Player
{
    public class PlayerMovement : Movement
    {
        public float rollSpeed = 10.0f;
        public float rollDuration = 0.5f;
        
        public bool IsRolling { get;private set; }

        public void Rotate(Vector3 direction)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 480);
        }

        public override void Move(Vector3 direction)
        {
            base.Move(direction);
            Vector3 resultPos = transform.position;
            resultPos.y = 0.0f;
            transform.position = resultPos;
        }
        
        public void Roll(Vector3 direction)
        {
            if (IsRolling) return;
            StartCoroutine(RollCoroutine(direction));
        }

        private IEnumerator RollCoroutine(Vector3 direction)
        {
            float timer = 0.0f;
            IsRolling = true;
            transform.rotation = Quaternion.LookRotation(direction);

            while (timer < rollDuration)
            {
                timer += Time.deltaTime;
                _controller.Move(direction.normalized * (rollSpeed * Time.deltaTime));

                Vector3 resultPos = transform.position;
                resultPos.y = 0.0f;
                transform.position = resultPos;
                yield return null;
            }
            
            IsRolling = false;
        }
    }
}
