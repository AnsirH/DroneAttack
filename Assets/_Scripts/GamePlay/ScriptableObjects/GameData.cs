using UnityEngine;

namespace _Scripts.GamePlay.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
    public class GameData : ScriptableObject
    {
        public float maxTime;
        public int[] droneCountsToSpawn;
        public int SpawnInterval => (int)maxTime / droneCountsToSpawn.Length;
    }
}
