using System;
using UnityEngine;

namespace MyCell.Weapon
{
    public class WeaponAnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinish;

        public void AnimationFinishTrigger()
        {
            OnFinish?.Invoke();
        }
    }
}