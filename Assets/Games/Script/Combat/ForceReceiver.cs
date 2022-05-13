using Sirenix.OdinInspector;
using UnityEngine;

namespace RPG.Combat
{
    public class ForceReceiver : MonoBehaviour
    {
        [ShowInInspector, SerializeField, Required]
        private CharacterController controller;

        private float verticalVelocity;
        public Vector3 Movement => Vector3.up * verticalVelocity;

        private void Update()
        {
            if (controller.isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = Physics.gravity.y * Time.deltaTime;
            }
            else
            {
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            }
        }
    }
}
