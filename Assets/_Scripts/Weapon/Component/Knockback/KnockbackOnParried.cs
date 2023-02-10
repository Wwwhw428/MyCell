using UnityEngine;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class KnockbackOnParried : WeaponKnockback<KnockbackOnParriedData>
    {
        private Parry parry;

        private WeaponKnockbackStruct currentAttackData;

        private void HandleParry(GameObject parriedObj)
        {
            CheckKnockback(parriedObj, currentAttackData);
        }
        
        public override void Init()
        {
            base.Init();
            
            parry = GetComponent<Parry>();
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
            currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            parry.OnParry += HandleParry;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            parry.OnParry -= HandleParry;
        }
    }

    public class KnockbackOnParriedData : WeaponComponentData<WeaponKnockbackStruct>
    {
        public KnockbackOnParriedData()
        {
            Components.Add(typeof(KnockbackOnParried));
        }
    }
}