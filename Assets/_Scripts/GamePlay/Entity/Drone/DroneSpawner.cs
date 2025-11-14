using System;
using System.Collections;
using _Scripts.GamePlay.ObjectPool;
using _Scripts.GamePlay.ScriptableObjects;
using UnityEngine;

namespace _Scripts.GamePlay.Entity.Drone
{
    public class DroneSpawner : MonoBehaviour
    {
        public DroneController dronePrefab;
        
        private DroneObjectPool _droneObjectPool;

        private void Awake()
        {
            _droneObjectPool = gameObject.AddComponent<DroneObjectPool>();
            _droneObjectPool.dronePrefab = dronePrefab;
        }

        public void Spawn(int count, Transform target)
        {
            for (int i = 0; i < count; i++)
            {
                DroneController drone = _droneObjectPool.Pool.Get();
                drone.transform.position = UnityEngine.Random.insideUnitSphere * 10;
                drone.Initialize(transform.position, 20);
                drone.SetTarget(target);
            }
        }
    }
}
