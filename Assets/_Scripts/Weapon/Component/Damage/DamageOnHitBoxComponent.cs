using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Weapon.Component.Data;
using MyCell.Structs;

namespace MyCell.Weapon.Component
{
    public class DamageOnHitBoxComponent : WeaponDamageComponent<DamageOnHitBoxData>
    {
        private WeaponActionHitboxComponent _hitbox;
        private CriticalDamageComponent _criticalDamage;
        private WeaponDamage _weaponDamage;

        public override void Init()
        {
            base.Init();

            _hitbox = GetComponent<WeaponActionHitboxComponent>();
            _criticalDamage = GetComponent<CriticalDamageComponent>();
            if (_hitbox == null)
                Debug.LogWarning("cant find WeaponActionHitboxComponent");

            _hitbox.OnDetected += DetectedHandler;
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();

            _weaponDamage = componentData.GetDataByIndex(currentAttackCount);
        }

        public void DetectedHandler(Collider2D[] detected)
        {
            foreach (var item in detected)
            {
                var damage = _criticalDamage.DamageHandler(item.gameObject, _weaponDamage.damageAmount);
                CheckDamage(item.gameObject, damage);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _hitbox.OnDetected -= DetectedHandler;

        }
    }

    public class DamageOnHitBoxData : WeaponComponentData<WeaponDamage>
    {
        public DamageOnHitBoxData()
        {
            Components.Add(typeof(DamageOnHitBoxComponent));
        }
    }
}
