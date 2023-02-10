using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class CancelOnInputRelease : WeaponComponent<CancelOnInputReleaseData>
    {
        private bool input;
        private bool minHoldPassed;

        private void HandleInputChange(bool value)
        {
            input = value;
            SetParams();
        }

        private void SetParams()
        {
            if (!input && !minHoldPassed) return;
            weapon.BaseAnim.SetBool(WeaponBoolAnimParameters.Hold.ToString(), input);
            weapon.BaseAnim.SetBool(WeaponBoolAnimParameters.Cancel.ToString(), !input);
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

    public class CancelOnInputReleaseData : WeaponComponentData
    {
        public CancelOnInputReleaseData()
        {
            Components.Add(typeof(CancelOnInputRelease));
        }
    }
}