using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class ComponentModifier<T> : WeaponComponent<T> where T: WeaponComponentData
    {
        protected WeaponModifiers modifiers;

        public override void Init()
        {
            base.Init();
            modifiers = GetComponent<WeaponModifiers>();
        }
    }
}