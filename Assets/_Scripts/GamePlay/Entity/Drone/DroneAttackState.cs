using UnityEngine;

namespace _Scripts.GamePlay.Entity.Drone
{
    public class DroneAttackState : DroneState
    {
        private Vector3 _direction;
        
        public DroneAttackState(DroneController droneController) : base(droneController)
        {
        }

        public override void Entered()
        {
            _direction = _droneController.Target.position - _droneController.transform.position;
        }

        public override void Updated()
        {
            _droneController.Movement.Move(_direction.normalized * _droneController.DashSpeedRatio);
        }

        public override void Exited()
        {
        }
    }
}
