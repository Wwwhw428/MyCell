using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine;

namespace MyCell.Weapon
{
    public class WeaponAnimationHandler : MonoBehaviour
    {
        public event Action OnAnimationFinished;
        public event Action<WeaponAttackPhase> OnAttackPhaseChanged;
        public event Action OnAttackStartMove;
        public event Action OnAttackStopMove;
        public event Action OnAttackAction;
        public event Action OnMinHold;
        public event Action OnDisableFlip;
        public event Action OnEnableFlip;
        public event Action OnEnableOptionalSprite;
        public event Action OnDisableOptionalSprite;
        public event Action OnEnableInterrupt;

        public void AnimationFinishTrigger()
        {
            OnAnimationFinished?.Invoke();
        }

        public void AttackPhaseChangeTrigger(WeaponAttackPhase phase)
        {
            OnAttackPhaseChanged?.Invoke(phase);
        }

        public void AttackStartMoveTrigger()
        {
            OnAttackStartMove?.Invoke();
        }

        public void AttackStopMoveTrigger()
        {
            OnAttackStopMove?.Invoke();
        }

        public void OnAttackActionTrigger()
        {
            OnAttackAction?.Invoke();
        }

        private void MinHoldTrigger()
        {
            OnMinHold?.Invoke();
        }

        private void DisableFlipTrigger()
        {
            OnDisableFlip?.Invoke();
        }

        private void EnableFlipTrigger()
        {
            OnEnableFlip?.Invoke();
        }

        private void EnableOptionalSpriteTrigger()
        {
            OnEnableOptionalSprite?.Invoke();
        }

        private void DisableOptionalSpriteTrigger()
        {
            OnDisableOptionalSprite?.Invoke();
        }

        private void EnableInterruptTrigger()
        {
            OnEnableInterrupt?.Invoke();
        }
    }
}
