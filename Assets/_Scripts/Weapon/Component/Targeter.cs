using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public class Targeter : WeaponComponent<TargeterData>
    {
        // This class is responsible for finding all game objects that are potential targets then firing off an event
        // with those targets

        public event Action<Collider2D[]> OnFindAllTargets;
        public event Action<Collider2D[]> OnFindAccesibleTargets;

        private WeaponModifiers modifiers;

        private AllTargetModifier allTargetModifier = new AllTargetModifier();
        private AccessibleTargetModifier accessibleTargetModifier = new AccessibleTargetModifier();

        private TargeterShape currentAttackData;

        private Movement _movement;
        public Movement Movement
        {
            get => _movement ?? core.GetCoreComponent(ref _movement);
            private set => _movement = value;
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();

            currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }

        private void DetermineAllTargets()
        {
            var pos = transform.position +
                      new Vector3(currentAttackData.Offset.x * Movement.CurrentFaceDirection, currentAttackData.Offset.y);

            var targets = Physics2D.OverlapBoxAll(pos, currentAttackData.Size, 0f, currentAttackData.damageableLayer);
            
            OnFindAllTargets?.Invoke(targets);

            if(targets.Length == 0) return;
            
            allTargetModifier.ModifierValue = targets;
            modifiers.AddModifier(allTargetModifier);
            
             CheckTargetAccessability(targets);
        }

        private void CheckTargetAccessability(Collider2D[] targets)
        {
            List<Collider2D> accessabile = new List<Collider2D>();

            foreach (Collider2D target in targets)
            {
                var hit = Physics2D.Linecast(transform.position, target.transform.position,
                    currentAttackData.groundLayer);

                if (!hit)
                {
                    accessabile.Add(target);
                }
            }

            var accessibileTargets = accessabile.ToArray();

            accessibleTargetModifier.ModifierValue = accessibileTargets;
            modifiers.AddModifier(accessibleTargetModifier);
            
            OnFindAccesibleTargets?.Invoke(accessibileTargets);
        }

        public override void Init()
        {
            base.Init();

            modifiers = GetComponent<WeaponModifiers>();
        }

        private void HandleInput(bool value)
        {
            if(!value) DetermineAllTargets();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            weapon.OnInputChange += HandleInput;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            weapon.OnInputChange -= HandleInput;
        }

        private void OnDrawGizmos()
        {
            if (!TryGetComponent(out Weapon wep)) return;
            if (!wep.WeaponData) return;
            var Data = wep.WeaponData.GetComponentData<TargeterData>();
            if (Data == null) return;
            var allData = Data.GetAllData();
            if (allData == null) return;

            foreach (TargeterShape shape in allData)
            {
                if(!shape.debug) continue;
                Gizmos.DrawWireCube(transform.position + (Vector3)shape.Offset, shape.Size);
            }
        }
    }
    
    public class TargeterData : WeaponComponentData<TargeterShape>
    {
        public TargeterData()
        {
            Components.Add(typeof(Targeter));
            Components.Add(typeof(WeaponModifiers));
        }
    }
}