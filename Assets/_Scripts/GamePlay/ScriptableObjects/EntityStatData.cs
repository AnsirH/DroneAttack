using UnityEngine;

namespace _Scripts.GamePlay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CharacterStatData", menuName = "Scriptable Objects/CharacterStatData")]
    public class EntityStatData : ScriptableObject
    {
        public float maxHealth = 10;
        public float attackPower = 5;
        public float moveSpeed = 5;
    }
}
