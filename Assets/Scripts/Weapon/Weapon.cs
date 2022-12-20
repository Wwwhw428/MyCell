using System;
using MyCell.ScriptableObjects.SO.Weapon;
using MyCell.Utilies;
using MyCell.Weapon.Components;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace MyCell.Weapon
{
    public class Weapon : MonoBehaviour
    {

        public WeaponDataSo WeaponData
        {
            get => weaponData;
            set
            {
                weaponData = value;
                GenerateWeapon();
            }
        }
        [SerializeField]
        private WeaponDataSo weaponData;

        #region Component Variable
        
        public Core.Core Core { get; private set; }
        [HideInInspector]
        public Animator baseAnim;
        private WeaponAnimationEventHandler _eventHandler;
        
        #endregion
        
        #region Event Variable

        public event Action<int> OnCounterChange;
        public event Action OnExit; 

        #endregion
        
        #region GameObject Variable
        
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }

        #endregion

        #region Other Variable

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            set
            {
                _currentAttackCounter = value >= WeaponData.numberOfAttack ? 0 : value;
                OnCounterChange?.Invoke(_currentAttackCounter);
            }
            
        }
        private int _currentAttackCounter;
        
        #endregion

        #region Unity Callback

        private void Awake()
        {
            Core = transform.Find("Core").GetComponent<Core.Core>();
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
            baseAnim = BaseGameObject.GetComponent<Animator>();
            _eventHandler = GetComponentInChildren<WeaponAnimationEventHandler>();
        }
        
        #endregion
        
        public void Enter()
        {
            baseAnim.SetBool("Active", true);
            baseAnim.SetInteger("Count", CurrentAttackCounter);
        }

        private void Exit()
        {
            baseAnim.SetBool("Active", false);
            CurrentAttackCounter++;
            OnExit?.Invoke();
        }

        private void OnEnable()
        {
            _eventHandler.OnFinish += Exit;
        }

        private void OnDisable()
        {
            _eventHandler.OnFinish -= Exit;
        }

        public void GenerateWeapon()
        {
            if (WeaponData == null)
            {
                Debug.LogError($"{this} has no associated data");
                return;
            }

            GameObjectUtilities.AddComponentsToGo<WeaponComponent>(gameObject, WeaponData.GetAllWeaponComponent());

            var baseGo = transform.Find("Base");
            
            if (!baseGo.TryGetComponent(out Animator anim))
            {
                anim = baseGo.gameObject.AddComponent<Animator>();
            }
            
            anim.runtimeAnimatorController = WeaponData.AnimatorController;
        }
    }

    [System.Serializable]
    public enum WeaponAttackPhase
    {
        Point,
        Hold,
        Backswing,
        Action,
        Cancel,
        Break,
        Parry
    }
}