using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public class WeaponMovementComponent : WeaponComponent<WeaponMovementData>
    {
        private Movement _movement;
        private WeaponAttackPhase _currentAttackPhase;
        private AttackPhaseMovement _movePhase;
        
        #region Unity Callback

        protected override void Awake() {
            base.Awake();

            _movement = core.GetCoreComponent(ref _movement);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            weapon.AnimationHandler.OnAttackPhaseChanged += SetPhase;
            weapon.AnimationHandler.OnAttackStartMove += StartMovement;
            weapon.AnimationHandler.OnAttackStopMove += StopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.AnimationHandler.OnAttackPhaseChanged -= SetPhase;
            weapon.AnimationHandler.OnAttackStartMove -= StartMovement;
            weapon.AnimationHandler.OnAttackStopMove -= StopMovement;
        }

        #endregion

        #region Set Function

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
        }

        private void SetPhase(WeaponAttackPhase phase)
        {
            _currentAttackPhase = phase;
            _movePhase = componentData.GetDataByIndex(currentAttackCount).attackPhases.FirstOrDefault(item => item.phase == phase);
        }

        #endregion

        private void StartMovement()
        {
            _movement.SetVelocity(_movePhase.speed, _movePhase.angle, _movement.CurrentFaceDirection);
        }

        private void StopMovement()
        {
            _movement.SetVelocityX(0f);
        }


    }

    public class WeaponMovementData : WeaponComponentData<WeaponMovement>
    {
        public WeaponMovementData()
        {
            Components.Add(typeof(WeaponMovementComponent));
        }

        public override void OnValidate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].attackPhases.Length; j++)
                {
                    data[i].attackPhases[j].name = data[i].attackPhases[j].phase.ToString();
                }
            }
        }
    }
}
