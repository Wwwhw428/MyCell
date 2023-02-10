using System;
using MyCell.Modifiers;
using UnityEngine;
using UnityEngine.Events;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public class WeaponBlockComponent : WeaponComponent<WeaponBlockData>
    {
        public event Action<GameObject> OnBlock;

        // Related Core Components
        private DamageComponent damageComponent;

        private DamageComponent DamageComponent =>
            damageComponent ? damageComponent : core.GetCoreComponent(ref damageComponent);

        private KnockbackComponent knockbackComponent;

        private KnockbackComponent KnockbackComponent =>
            knockbackComponent ? knockbackComponent : core.GetCoreComponent(ref knockbackComponent);

        private PoiseDamageComponent poiseDamageComponent;

        private PoiseDamageComponent PoiseDamageComponent => poiseDamageComponent
            ? poiseDamageComponent
            : core.GetCoreComponent(ref poiseDamageComponent);

        private Movement movement;
        private Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);

        private ParticleManager particleManager;

        private ParticleManager ParticleManager =>
            particleManager ? particleManager : core.GetCoreComponent(ref particleManager);

        private WeaponBlock currentAttackData;

        // Modifiers
        private BlockDamageModifier damageModifier;
        private BlockKnockbackModifier knockbackModifier;
        private BlockPoiseDamageModifier poiseDamageModifier;

        private bool isBlockWindowActive;
        private bool shouldCheckBlockTime;

        private void HandleEnterAttackPhase(WeaponAttackPhase phase)
        {
            if (!isBlockWindowActive)
            {
                shouldCheckBlockTime = currentAttackData.BlockWindowStart.SetTriggerTime(phase);
            }
            else
            {
                shouldCheckBlockTime = currentAttackData.BlockWindowEnd.SetTriggerTime(phase);
            }
        }

        private void Update()
        {
            if (!shouldCheckBlockTime)
                return;

            if (isBlockWindowActive)
            {
                if (currentAttackData.BlockWindowEnd.IsPastTriggerTime)
                    DisableBlockWindow();
            }
            else
            {
                if (currentAttackData.BlockWindowStart.IsPastTriggerTime)
                    EnableBlockWindow();
            }
        }

        private void EnableBlockWindow()
        {
            isBlockWindowActive = true;

            damageModifier.OnBlock += HandleSuccessfulBlock;

            DamageComponent.DamageModifiers.AddModifier(damageModifier);
            KnockbackComponent.KnockbackModifiers.AddModifier(knockbackModifier);
            PoiseDamageComponent.PoiseDamageModifiers.AddModifier(poiseDamageModifier);
            shouldCheckBlockTime = false;
        }

        private void DisableBlockWindow()
        {
            isBlockWindowActive = false;

            damageModifier.OnBlock -= HandleSuccessfulBlock;

            DamageComponent.DamageModifiers.RemoveModifier(damageModifier);
            KnockbackComponent.KnockbackModifiers.RemoveModifier(knockbackModifier);
            PoiseDamageComponent.PoiseDamageModifiers.RemoveModifier(poiseDamageModifier);
            shouldCheckBlockTime = false;
        }

        private void HandleSuccessfulBlock(GameObject blockedObj)
        {
            ParticleManager.StartParticlesWithRandomRotation(currentAttackData.BlockParticles,
                currentAttackData.BlockParticlesOffset);

            OnBlock?.Invoke(blockedObj);
        }

        private void Start()
        {
            damageModifier = new BlockDamageModifier(currentAttackData, Movement,
                core.EntityTransform);

            knockbackModifier =
                new BlockKnockbackModifier(currentAttackData, Movement, core.EntityTransform);

            poiseDamageModifier = new BlockPoiseDamageModifier(currentAttackData, Movement, core.EntityTransform);
        }


        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
            currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            weapon.AnimationHandler.OnAttackPhaseChanged += HandleEnterAttackPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            weapon.AnimationHandler.OnAttackPhaseChanged -= HandleEnterAttackPhase;
        }
    }

    public class WeaponBlockData : WeaponComponentData<WeaponBlock>
    {
        public WeaponBlockData()
        {
            Components.Add(typeof(WeaponBlockComponent));
        }
    }
}