namespace _Scripts.GamePlay.Entity.Drone
{
    public class StateMachine
    {
        private readonly DroneController _droneController;
        
        public DroneState CurrentState { get; private set; }
        
        public StateMachine(DroneController droneController)
        {
            _droneController = droneController;
        }

        public void Transition(DroneState newState)
        {
            if (CurrentState != null)
                CurrentState.Exited();
            CurrentState = newState;
            CurrentState.Entered();
        }
    }
}
