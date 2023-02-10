using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Weapon.Component.Data;
using MyCell.Structs;
using MyCell.Utilities;

namespace MyCell.Weapon.Component
{
    public class WeaponDamageComponent<T> : WeaponComponent<T> where T : WeaponComponentData
    {
        private DamageData _data;
        public void CheckDamage(GameObject obj, int damageAmount)
        {
            _data.SetData(core.transform.parent.gameObject, damageAmount);
            CombatUtilities.CheckIfDamageable(obj, _data, out _);
        }
    }
}
