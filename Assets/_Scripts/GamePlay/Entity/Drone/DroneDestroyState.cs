using System;

namespace _Scripts.GamePlay.Entity.Drone
{
    public class DroneDestroyState : DroneState
    {
        public DroneDestroyState(DroneController droneController) : base(droneController)
        {
        }

        public override void Entered()
        {
            // 파괴 이펙트 재생
            _droneController.explosionParticle.Play();
            _droneController.model.SetActive(false);
        }

        public override void Updated()
        {
            if (!_droneController.explosionParticle.isPlaying)
            {
                if (!_droneController.IsReleased)
                    _droneController.Pool.Release(_droneController);
            }
        }

        public override void Exited()
        {
        }
    }
}
