using System;
using System.Collections;
using _Scripts.GamePlay.Entity.Drone;
using _Scripts.GamePlay.ScriptableObjects;
using UnityEngine;

namespace _Scripts.GamePlay.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] DroneSpawner droneSpawner;
        
        public GameData gameData;
        public Transform player;

        private IEnumerator PlayRound()
        {
            for (int i = 0; i < gameData.droneCountsToSpawn.Length; ++i)
            {
                droneSpawner.Spawn(gameData.droneCountsToSpawn[i], player);

                yield return new WaitForSeconds(gameData.SpawnInterval);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Spawn Drone"))
            {
                StartCoroutine(PlayRound());
            }
        }
    }
}
