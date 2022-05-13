using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StateMachine
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingLookBlendTree = Animator.StringToHash("TargetingBlendTree");

        private readonly int TargetingForwardSpeed = Animator.StringToHash("TargetingForwardSpeed");
        private readonly int TargetingRightSpeed = Animator.StringToHash("TargetingRightSpeed");

        private const float AnimatorDampTime = .1f;

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.CancelEvent += CancelEvent;
            stateMachine.InputReader.TargetEvent += TargetEvent;

            stateMachine.TargetGroup.AddMember(stateMachine.Targeter.currentTarget.transform, 1, 2);
            stateMachine.Animator.Play(TargetingLookBlendTree);
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.Targeter.currentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }

            Vector3 movement = CalculateMovement();
            Move(movement * stateMachine.TargetMovementSpeed, deltaTime);

            stateMachine.Animator.SetFloat(TargetingForwardSpeed, stateMachine.InputReader.movementValue.y == 0 ? 0 : 1, AnimatorDampTime, deltaTime);
            stateMachine.Animator.SetFloat(TargetingRightSpeed, stateMachine.InputReader.movementValue.x == 0 ? 0 : 1, AnimatorDampTime, deltaTime);

            FaceTarget();
        }

        private Vector3 CalculateMovement()
        {
            Vector3 movement = new Vector3();

            movement += stateMachine.transform.right * stateMachine.InputReader.movementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.movementValue.y;

            return movement;
        }

        public override void Exit()
        {
            stateMachine.InputReader.CancelEvent -= CancelEvent;
            stateMachine.InputReader.TargetEvent -= TargetEvent;

            if (stateMachine.Targeter.currentTarget == null)
            {
                stateMachine.TargetGroup.RemoveMember(null);
            }
            else
            {
                stateMachine.TargetGroup.RemoveMember(stateMachine.Targeter.currentTarget.transform);
            }
            stateMachine.Targeter.Cancel();
            Debug.Log("Target Exit");
        }

        private void TargetEvent()
        {
            stateMachine.TargetGroup.RemoveMember(stateMachine.Targeter.currentTarget.transform);
            stateMachine.Targeter.NextTarget();
            stateMachine.TargetGroup.AddMember(stateMachine.Targeter.currentTarget.transform, 1, 2);
        }

        private void CancelEvent()
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
