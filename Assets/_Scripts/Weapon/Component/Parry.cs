using System.ComponentModel.Design;
using System;
using UnityEngine;
using UnityEngine.Events;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;
using MyCell.Utilities;

namespace MyCell.Weapon.Component
{
    public class Parry : WeaponComponent<ParryData>
    {
        public event Action<GameObject> OnParry;

        private WeaponParry _currentAttackData;

        private float _parryWindowStartTime;
        private float _parryWindowEndTime;

        private bool _shouldCheckParryTime;
        private bool _isParryWindowActive;

        private Vector2 _offset;

        private Collider2D[] _detected;

        private Movement _movement;
        private Movement Movement => _movement ? _movement : core.GetCoreComponent(ref _movement);

        private ParticleManager _particleManager;

        private ParticleManager ParticleManager =>
            _particleManager ? _particleManager : core.GetCoreComponent(ref _particleManager);

        private void HandleEnter()
        {
            _parryWindowStartTime = Time.time + _currentAttackData.ParryWindowStart;
            _parryWindowEndTime = Time.time + _currentAttackData.ParryWindowEnd;
            _shouldCheckParryTime = true;
        }

        private void Update()
        {
            if (!_shouldCheckParryTime)
                return;

            if (!_isParryWindowActive && Time.time >= _parryWindowStartTime)
                EnableParryWindow();

            if (_isParryWindowActive && Time.time >= _parryWindowEndTime)
                DisableParryWindow();

            CheckParryHitbox();
        }

        private void CheckParryHitbox()
        {
            if (!_isParryWindowActive) return;

            _offset.Set(
                transform.position.x + (_currentAttackData.ParryHitbox.position.x * Movement.CurrentFaceDirection),
                transform.position.y + _currentAttackData.ParryHitbox.y
            );

            _detected = Physics2D.OverlapBoxAll(_offset, _currentAttackData.ParryHitbox.size, 0f, componentData.ParryableLayer);

            if (_detected.Length == 0) return;

            var parryData = new MyCell.Interfaces.ParryData
            {
                source = gameObject
            };

            //Check if detected are Parryable
            foreach (var item in _detected)
            {
                if (CombatUtilities.CheckIfParryable(item, parryData, out var parryable))
                {
                    ParticleManager.StartParticlesWithRandomRotation(componentData.ParryParticlesPrefab,
                        componentData.ParryParticlesOffset);
                    OnParry?.Invoke(parryable.GetParent());
                    weapon.BaseAnim.SetTrigger(WeaponTriggerAnimParameters.Parry.ToString());
                    DisableParryWindow();
                }
            }
        }

        private void EnableParryWindow()
        {
            _isParryWindowActive = true;
        }

        private void DisableParryWindow()
        {
            _isParryWindowActive = false;
            _shouldCheckParryTime = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            weapon.OnEnter -= HandleEnter;
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
            _currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }
    }

    public class ParryData : WeaponComponentData<WeaponParry>
    {
        [field: SerializeField] public GameObject ParryParticlesPrefab { get; private set; }
        [field: SerializeField] public Vector2 ParryParticlesOffset { get; private set; }
        [field: SerializeField] public LayerMask ParryableLayer { get; private set; }

        public ParryData()
        {
            Components.Add(typeof(Parry));
        }
    }
}