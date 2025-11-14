using System.Collections.Generic;
using _Scripts.GamePlay.Entity.Drone;
using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.GamePlay.ObjectPool
{
    public class DroneObjectPool : MonoBehaviour
    {
        public DroneController dronePrefab;
        public int maxPoolSize = 10;
        public int stackDefaultCapacity = 10;

        public IObjectPool<DroneController> Pool
        {
            get
            {
                if (_pool == null)
                    _pool = new ObjectPool<DroneController>(
                        CreatedPooledItem,
                        OnTakeFromPool,
                        OnReturnToPool,
                        OnDestroyPoolObject,
                        true,
                        stackDefaultCapacity,
                        maxPoolSize);
                return _pool;
            }
        }
        
        private IObjectPool<DroneController> _pool;

        private DroneController CreatedPooledItem()
        {
            DroneController newDrone = Instantiate(dronePrefab);
            
            newDrone.gameObject.name = "Drone";
            newDrone.Pool = Pool;
            
            return newDrone;
        }

        private void OnReturnToPool(DroneController drone)
        {
            drone.gameObject.SetActive(false);
            drone.SetIsReleased(true);
        }

        private void OnTakeFromPool(DroneController drone)
        {
            drone.gameObject.SetActive(true);
            drone.SetIsReleased(false);
        }

        private void OnDestroyPoolObject(DroneController drone)
        {
            Destroy(drone.gameObject);
        }
    }
}