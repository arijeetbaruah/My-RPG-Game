using UnityEngine;

namespace RPG.StateMachine
{
    public abstract class StateMachine : MonoBehaviour
    {
        protected IState currentState;

        public void SwitchState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        protected void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }
    }
}
