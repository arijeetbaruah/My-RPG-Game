using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat.Targeting
{
    public class Targeter : MonoBehaviour
    {
        [ShowInInspector]
        private List<Target> targets = new List<Target>();

        private int currentTargetIndex = -1;

        public Target currentTarget => currentTargetIndex == -1 ? null : targets[currentTargetIndex];
        public event System.Action<Target> TargetRemoved;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Target>(out Target target))
            {
                target.OnDestroyEvent += OnTargetDestory;

                targets.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Target>(out Target target))
            {
                target.OnDestroyEvent -= OnTargetDestory;

                TargetRemoved?.Invoke(target);
                targets.Remove(target);
            }
        }

        private void OnTargetDestory(Target target)
        {
            if (target == currentTarget)
            {
                TargetRemoved?.Invoke(target);
                currentTargetIndex = -1;
            }

            target.OnDestroyEvent -= OnTargetDestory;
            targets.Remove(target);
        }

        public bool SelectTarget()
        {
            if (targets.Count == 0) return false;

            currentTargetIndex = 0;

            return true;
        }

        public bool NextTarget()
        {
            if (targets.Count == 0) return false;

            currentTargetIndex++;
            if (currentTargetIndex == targets.Count)
            {
                currentTargetIndex = 0;
            }

            return true;
        }

        public void Cancel()
        {
            currentTargetIndex = -1;
        }
    }
}
