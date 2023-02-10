using UnityEngine;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class KnockbackOnHitboxAction : WeaponKnockback<KnockbackOnHitboxActionData>
    {
        private WeaponActionHitboxComponent hitbox;

        private WeaponKnockbackStruct currentAttackData;

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
            currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }

        public override void Init()
        {
            base.Init();
            hitbox = GetComponent<WeaponActionHitboxComponent>();
        }

        private void HandleDetected(Collider2D[] detected)
        {
            foreach (var item in detected)
            {
                CheckKnockback(item.gameObject, currentAttackData);
            }
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            hitbox.OnDetected += HandleDetected;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            hitbox.OnDetected -= HandleDetected;
        }  
    }

    public class KnockbackOnHitboxActionData : WeaponComponentData<WeaponKnockbackStruct>
    {
        public KnockbackOnHitboxActionData()
        {
            Components.Add(typeof(KnockbackOnHitboxAction));
        }
    }
}