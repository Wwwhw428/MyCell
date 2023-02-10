using UnityEngine;
using MyCell.Utilities;
using MyCell.Interfaces;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public abstract class WeaponKnockback<T> : WeaponComponent<T> where T : WeaponComponentData
    {
        private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
        private Movement _movement;

        protected void CheckKnockback(GameObject obj, WeaponKnockbackStruct data)
        {
            var knockbackData = new KnockbackData(data.KnockbackAngle,
                data.KnockbackStrength, Movement.CurrentFaceDirection, gameObject);

            CombatUtilities.CheckIfKnockbackable(obj, knockbackData, out _);
        }
    }
}