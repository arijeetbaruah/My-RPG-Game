using UnityEngine;

namespace RPG.StateMachine
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private bool isRunning = false;
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private readonly int FreeLookBlendTree = Animator.StringToHash("FreeLookBlendTree");
        private const float AnimatorDampTime = .1f;

        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.InputReader.RunEvent += RunEvent;
            stateMachine.InputReader.TargetEvent += TargetEvent;

            stateMachine.Animator.Play(FreeLookBlendTree);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = CalculateMovement();
            Move(movement * (isRunning ? stateMachine.MovementSpeed : stateMachine.RunningSpeed), deltaTime);
            if (stateMachine.InputReader.movementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                return;
            }
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, isRunning ? 1f : 0.5f, AnimatorDampTime, deltaTime);
            FaceRotationControl(movement, deltaTime);
        }

        public override void Exit()
        {
            stateMachine.InputReader.RunEvent -= RunEvent;
            stateMachine.InputReader.TargetEvent -= TargetEvent;
        }

        private void RunEvent()
        {
            isRunning = !isRunning;
        }

        private void TargetEvent()
        {
            if (stateMachine.Targeter.SelectTarget())
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
        }

        private void FaceRotationControl(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(movement), deltaTime * stateMachine.RoationSmoothValue);
        }

        private Vector3 CalculateMovement()
        {
            var forward = stateMachine.cameraTransform.forward;
            var right = stateMachine.cameraTransform.right;

            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.movementValue.y
                + right * stateMachine.InputReader.movementValue.x;
        }
    }
}
