using UnityEngine;
using System.Linq;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;
using MyCell.Interfaces;
using MyCell.Managers;


namespace MyCell.Weapon.Component
{
    public class CriticalDamageComponent : WeaponComponent<CriticalDamageData>
    {
        private CriticalDamage _criticalDamage;
        private PhaseCriticalDamage _currentCriticalData;
        private IsCritical _criticalCheck;
        private CriticalManager _criticalManager;

        public override void Init()
        {
            base.Init();

            _criticalCheck = GetComponent<IsCritical>();
            _criticalManager = GameObject.Find("CriticalManager").GetComponent<CriticalManager>();
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();

            _criticalDamage = componentData.GetDataByIndex(currentAttackCount);
        }

        public void SetPhase(WeaponAttackPhase phase)
        {
            _currentCriticalData = _criticalDamage.criticalData.FirstOrDefault(item => item.phase == phase);
        }

        public int DamageHandler(GameObject obj, int DamageAmount)
        {
            int res;
            
            if (_criticalCheck.CheckIfCritical())
            {
                res = (DamageAmount + _currentCriticalData.criticalAdd) * _currentCriticalData.criticalMul;
                _criticalManager.CriticalHandler(obj);
            }
            else
                res = DamageAmount;

            return res;
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

    public class CriticalDamageData : WeaponComponentData<CriticalDamage>
    {
        public CriticalDamageData()
        {
            Components.Add(typeof(CriticalDamageComponent));
        }

        public override void OnValidate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].criticalData.Length; j++)
                {
                    data[i].criticalData[j].name = data[i].criticalData[j].phase.ToString();
                }
            }
        }
    }
}