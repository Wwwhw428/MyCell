using System;
using UnityEngine;
using MyCell.SO.Weapon;
using MyCell.Utilities;
using MyCell.Weapon.Component;

namespace MyCell.Weapon
{
    public class Weapon : MonoBehaviour
    {
        #region Event Variable

        public event Action OnEnter;
        public event Action OnExit;
        public event Action<int> OnCounterChange;
        public event Action<bool> OnInputChange;
        private WeaponAnimationHandler _animationHandler;
        public WeaponAnimationHandler AnimationHandler
        {
            get => _animationHandler ? _animationHandler : GetComponentInChildren<WeaponAnimationHandler>();
            set => _animationHandler = value;
        }

        #endregion

        #region GameObject Variable
        [HideInInspector] public GameObject BaseGo;
        [HideInInspector] public GameObject WeaponSpriteGo;

        #endregion

        #region Component Variable

        [HideInInspector] public Animator BaseAnim;
        [HideInInspector] public CoreSystem.Core core;

        #endregion

        #region Other Variable

        public int CurrentAttackCount
        {
            get => _currentAttackCount;
            private set
            {
                _currentAttackCount = value >= WeaponData.NumberOfAttack ? 0 : value;
                OnCounterChange?.Invoke(_currentAttackCount);
            }
        }
        private int _currentAttackCount;

        public bool CurrentInput
        {
            get => _currentInput;
            private set
            {
                if (value != _currentInput)
                    OnInputChange?.Invoke(value);
                _currentInput = value;
            }
        }
        private bool _currentInput;
        #endregion

        public WeaponDataSo WeaponData
        {
            get => _weaponData;
            set
            {
                _weaponData = value;
                GenerateWeapon();
            }
        }
        [field: SerializeField] private WeaponDataSo _weaponData;

        #region Unity Callback

        private void Awake()
        {
            BaseGo = transform.Find("Base").gameObject;
            WeaponSpriteGo = transform.Find("WeaponSprite").gameObject;
            BaseAnim = BaseGo.GetComponent<Animator>();
            AnimationHandler = BaseGo.GetComponent<WeaponAnimationHandler>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            //TODO： 不确定是不是应该加OnExit 或 Exit
            AnimationHandler.OnAnimationFinished += Exit;
        }

        private void OnDisable()
        {
            AnimationHandler.OnAnimationFinished -= Exit;
        }

        #endregion

        public void Init(CoreSystem.Core core)
        {
            this.core = core;
        }

        public void Enter()
        {
            gameObject.SetActive(true);
            BaseAnim.SetBool("Active", true);
            BaseAnim.SetInteger("Count", CurrentAttackCount);
            OnEnter.Invoke();
            OnInputChange?.Invoke(CurrentInput);
        }

        public void Exit()
        {
            BaseAnim.SetBool("Active", false);
            CurrentAttackCount++;
            OnExit.Invoke();
            gameObject.SetActive(false);
        }

        // TODO: GenerateWeapon
        public void GenerateWeapon()
        {
            CurrentAttackCount = 0;

            if (WeaponData == null)
            {
                Debug.LogError($"{this} has no associated data");
                return;
            }

            // TODO: add component
            var components = gameObject.AddComponentsToGO<WeaponComponent>(WeaponData.GetAllComponents());
            BaseAnim.runtimeAnimatorController = WeaponData.AnimatorController;

            components.ForEach(item => item.Init());
        }
    }

    public enum WeaponBoolAnimParameters
    {
        Active,
        Hold,
        Cancel,
    }

    public enum WeaponTriggerAnimParameters
    {
        Parry,
    }

    public enum WeaponIntAnimParameters
    {
        Counter,
    }

    public enum WeaponAttackPhase
    {
        Point, // 前摇
        Hold,
        Backswing, // 后摇
        Action,
        Cancel,
        Break,
        Parry
    }
}
