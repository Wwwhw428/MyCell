using UnityEngine;
using System.Linq;
using MyCell.Structs;
using MyCell.Interfaces;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class AttackAmountCriticalCheck : WeaponComponent<AttackAmountCriticalComponentData>, IsCritical
    {
        private AttackAmountCriticalData _attackAmountCriticalData;
        private AttackAmountCriticalPhase _currentPhase;

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();

            _attackAmountCriticalData = componentData.GetDataByIndex(currentAttackCount);
        }

        public void SetPhase(WeaponAttackPhase phase)
        {
            _currentPhase = _attackAmountCriticalData.phases.FirstOrDefault(item => item.phase == phase);
        }

        public bool CheckIfCritical()
        {
            return _currentPhase.isCritical;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            weapon.AnimationHandler.OnAttackPhaseChanged += SetPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            weapon.AnimationHandler.OnAttackPhaseChanged -= SetPhase;
        }
    }

    public class AttackAmountCriticalComponentData : WeaponComponentData<AttackAmountCriticalData>
    {
        public AttackAmountCriticalComponentData()
        {
            Components.Add(typeof(AttackAmountCriticalCheck));
        }

        public override void OnValidate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].phases.Length; j++)
                {
                    data[i].phases[j].name = data[i].phases[j].phase.ToString();
                }
            }
        }
    }
}
