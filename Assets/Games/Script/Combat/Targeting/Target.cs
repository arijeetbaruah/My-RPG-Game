using System;
using UnityEngine;

namespace RPG.Combat.Targeting
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnDestroyEvent;

        private void OnDestroy()
        {
            OnDestroyEvent?.Invoke(this);
        }
    }
}
