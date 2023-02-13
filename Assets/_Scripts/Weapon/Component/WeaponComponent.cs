using UnityEngine;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        protected CoreSystem.Core core;

        protected int currentAttackCount;

        protected float attackStartTime;



        public virtual void Init()
        {
            currentAttackCount = 0;

            weapon = GetComponent<Weapon>();
            core = weapon.core;

            // eventHandler = GetComponentInChildren<WeaponAnimationEventHandler>();
        }

        #region Unity Callback

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();
            core = weapon.core;
        }

        protected virtual void OnEnable()
        {
            weapon.OnCounterChange += setCount;
            weapon.OnEnter += SetStartTime;
        }

        protected virtual void OnDisable()
        {
            weapon.OnCounterChange -= setCount;
            weapon.OnEnter -= SetStartTime;
        }

        #endregion

        #region Set Function
        protected virtual void SetStartTime() => attackStartTime = Time.time;

        public void setCount(int value) => currentAttackCount = value;

        #endregion

    }
    public class WeaponComponent<T> : WeaponComponent where T : WeaponComponentData
    {
        protected T componentData;

        public override void Init()
        {
            base.Init();
            componentData = weapon.WeaponData.GetComponentData<T>();
        }

        protected virtual void SetCurrentAttackData() { }

        protected override void OnEnable()
        {
            base.OnEnable();
            weapon.OnEnter += SetCurrentAttackData;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            weapon.OnExit -= SetCurrentAttackData;
        }
    }
}
