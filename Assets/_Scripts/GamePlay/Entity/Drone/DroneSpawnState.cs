namespace _Scripts.GamePlay.Entity.Drone
{
    public class DroneSpawnState : DroneState
    {
        public DroneSpawnState(DroneController droneController) : base(droneController)
        {
        }

        public override void Entered()
        {
            // 스폰 이펙트 재생
            // 잠시 대기
            _droneController.ChangeState(EDroneState.MoveToTarget);
        }

        public override void Updated()
        {
        }

        public override void Exited()
        {
        }
    }
}
