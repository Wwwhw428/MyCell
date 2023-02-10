using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class WeaponInputHold : WeaponComponent<WeaponHoldData>
    {
        private bool minHoldPassed;
        private bool input;

        private void HandleInputChange(bool value)
        {
            input = value;
            SetParams();
        }

        private void SetParams()
        {
            if (!input && !minHoldPassed) return;
            weapon.BaseAnim.SetBool(WeaponBoolAnimParameters.Hold.ToString(), input);
        }

        private void Enter()
        {
            minHoldPassed = false;
        }

        private void MinHoldPassed()
        {
            minHoldPassed = true;
            SetParams();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.OnInputChange += HandleInputChange;
            weapon.OnEnter += Enter;
            weapon.AnimationHandler.OnMinHold += MinHoldPassed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.OnInputChange -= HandleInputChange;
            weapon.OnEnter -= Enter;
            weapon.AnimationHandler.OnMinHold -= MinHoldPassed;
        }
    }

    public class WeaponHoldData : WeaponComponentData
    {
        public WeaponHoldData()
        {
            Components.Add(typeof(WeaponInputHold));
        }
    }
}