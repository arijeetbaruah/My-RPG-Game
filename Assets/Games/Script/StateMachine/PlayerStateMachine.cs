using Cinemachine;
using RPG.Combat.Targeting;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.StateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        [TitleGroup("Serialized Fields")]

        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected Input.InputReader inputReader;

        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected CharacterController characterController;
        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected Animator animator;
        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected Targeter targeter;
        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected CinemachineTargetGroup targetGroup;
        [SceneObjectsOnly, ShowInInspector, SerializeField]
        protected Combat.ForceReceiver forceReceiver;

        [TitleGroup("Player Speed")]
        [MinValue(0), ShowInInspector, SerializeField]
        protected float movementSpeed;
        [MinValue(0), ShowInInspector, SerializeField]
        protected float runningSpeed;
        [MinValue(0), ShowInInspector, SerializeField]
        protected float targetMovementSpeed;
        [MinValue(0), ShowInInspector, SerializeField]
        protected float roationSmoothValue;

        public float MovementSpeed => movementSpeed;
        public float RunningSpeed => runningSpeed;
        public float TargetMovementSpeed => targetMovementSpeed;
        public float RoationSmoothValue => roationSmoothValue;
        public Transform cameraTransform => Camera.main.transform;

        public Input.InputReader InputReader => inputReader;
        public CharacterController CharacterController => characterController;
        public Animator Animator => animator;
        public Targeter Targeter => targeter;
        public CinemachineTargetGroup TargetGroup => targetGroup;
        public Combat.ForceReceiver ForceReceiver => forceReceiver;

        private void Start()
        {
            SwitchState(new PlayerFreeLookState(this));
            targeter.TargetRemoved += OnTargetRemove;
        }

        protected void OnTargetRemove(Target target)
        {
            targetGroup.RemoveMember(target.transform);
        }
    }
}
