using UnityEngine;

namespace _Scripts.GamePlay.Entity.Drone
{
    public class DroneMoveState : DroneState
    {
        public DroneMoveState(DroneController droneController) : base(droneController)
        {
        }

        public override void Entered()
        {
            
        }

        public override void Updated()
        {
            Vector3 direction = _droneController.Target.position - _droneController.transform.position;
            if (direction.sqrMagnitude > _droneController.AttackRange * _droneController.AttackRange)
                _droneController.Movement.Move(direction.normalized);
            else
                _droneController.ChangeState(EDroneState.DashAttack);
        }

        public override void Exited()
        {
        }
    }
}
