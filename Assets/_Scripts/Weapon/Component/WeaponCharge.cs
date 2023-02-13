using MyCell.Structs;
using MyCell.Utilities.Notifiers;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public class WeaponCharge : WeaponComponent<WeaponChargeData>
    {
        private WeaponModifiers weaponModifiers;
        private readonly ChargeModifier chargeModifier = new ChargeModifier();

        private ParticleManager _particleManager;
        private ParticleManager ParticleManager
        {
            get => _particleManager ?? core.GetCoreComponent(ref _particleManager);
        }

        private TimerNotifier timer;

        private int currentCharge;

        private void HandleInputChange(bool value)
        {
            if (value) return;

            if (currentCharge == 0)
                weapon.BaseAnim.SetBool(WeaponBoolAnimParameters.Cancel.ToString(), true);

            timer.Stop();
            chargeModifier.ModifierValue = currentCharge;
            weaponModifiers.AddModifier(chargeModifier);
        }

        private void SetCancelFalse() => weapon.BaseAnim.SetBool(WeaponBoolAnimParameters.Cancel.ToString(), false);

        public override void Init()
        {
            base.Init();

            weaponModifiers = GetComponent<WeaponModifiers>();
        }

        protected override void SetStartTime()
        {
            base.SetStartTime();

            var curAtkDat = componentData.GetDataByIndex(currentAttackCount);

            currentCharge = curAtkDat.StartWithOne ? 1 : 0;

            timer = new TimerNotifier(curAtkDat.ChargeTime, true);
            timer.OnTimerDone += AddCharge;
        }

        private void Update()
        {
            timer?.Tick();
        }

        private void AddCharge()
        {
            currentCharge++;

            var curDat = componentData.GetDataByIndex(currentAttackCount);

            if (currentCharge >= curDat.NumOfCharges)
            {
                timer.Stop();
                ParticleManager.StartParticles(curDat.MaxChargeParticlesPrefab, curDat.Offset);
            }
            else
            {
                ParticleManager.StartParticles(curDat.ChargeParticlesPrefab, curDat.Offset);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.OnInputChange += HandleInputChange;
            weapon.OnEnter += SetStartTime;
            weapon.OnExit += SetCancelFalse;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.OnInputChange -= HandleInputChange;
            weapon.OnEnter -= SetStartTime;
            weapon.OnExit -= SetCancelFalse;
        }
    }

    public class WeaponChargeData : WeaponComponentData<ChargeStruct>
    {
        public WeaponChargeData()
        {
            Components.Add(typeof(WeaponModifiers));
            Components.Add(typeof(WeaponInputHold));
            Components.Add(typeof(WeaponCharge));
        }
    }
}