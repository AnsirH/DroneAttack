namespace _Scripts.GamePlay.Entity.Drone
{
    public abstract class DroneState
    {
        protected DroneController _droneController;

        public DroneState(DroneController droneController)
        {
            _droneController = droneController;
        }
        
        public abstract void Entered();
        public abstract void Updated();
        public abstract void Exited();
    }
}
