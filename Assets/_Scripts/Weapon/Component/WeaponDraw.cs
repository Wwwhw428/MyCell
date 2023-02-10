using UnityEngine;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class WeaponDraw : WeaponComponent<WeaponDrawData>
    {
        private WeaponModifiers weaponModifiers;
        private DrawModifier drawModifier = new DrawModifier();

        private void HandleInputChange(bool value)
        {
            if (!value)
            {
                var curAtkData = componentData.GetDataByIndex(currentAttackCount);

                var evaluatedValue = curAtkData.curve.Evaluate(Mathf.Clamp((Time.time - attackStartTime) / curAtkData.drawTime, 0f, 1f));
                drawModifier.ModifierValue = evaluatedValue;
                weaponModifiers.AddModifier(drawModifier);
            }
        }

        public override void Init()
        {
            base.Init();

            weaponModifiers = GetComponent<WeaponModifiers>();
        }


        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.OnInputChange += HandleInputChange;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.OnInputChange -= HandleInputChange;

        }
    }

    public class WeaponDrawData : WeaponComponentData<DrawStruct>
    {
        public WeaponDrawData()
        {
            Components.Add(typeof(WeaponDraw));
            Components.Add(typeof(WeaponInputHold));
            Components.Add(typeof(WeaponModifiers));
        }
    }
}