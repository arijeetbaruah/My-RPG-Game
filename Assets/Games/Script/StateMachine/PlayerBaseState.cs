using UnityEngine;

namespace RPG.StateMachine
{
    public abstract class PlayerBaseState : IState
    {
        protected PlayerStateMachine stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Tick(float deltaTime);

        protected void Move(Vector3 motion, float deltaTime)
        {
            stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
        }

        protected void FaceTarget()
        {
            if (stateMachine.Targeter.currentTarget == null) return;

            Vector3 directionVector = stateMachine.Targeter.currentTarget.transform.position - stateMachine.CharacterController.transform.position;
            directionVector.y = 0;
            directionVector.Normalize();

            stateMachine.transform.rotation = Quaternion.LookRotation(directionVector);
        }
    }
}
