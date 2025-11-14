using System;
using _Scripts.GamePlay.Entity.Drone;
using _Scripts.GamePlay.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.GamePlay.Test
{
    public class TestDroneSpawn : MonoBehaviour
    {
        public DroneController dronePrefab;
        public Transform target;

        public float attackRange = 5.0f;
        public float dashSpeedRatio = 1.5f;

        private DroneObjectPool _dronePool;
        private void Awake()
        {
            _dronePool = gameObject.AddComponent<DroneObjectPool>();
            _dronePool.dronePrefab = dronePrefab;
        }

        private void SpawnDrone()
        {
            DroneController drone = _dronePool.Pool.Get();
            drone.transform.position = Random.insideUnitSphere * 10;
            drone.SetTarget(target);
            drone.Initialize(Vector3.zero, 30, dashSpeedRatio, attackRange);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 30), "Spawn Drone"))
            {
                SpawnDrone();
            }
        }
    }
}
