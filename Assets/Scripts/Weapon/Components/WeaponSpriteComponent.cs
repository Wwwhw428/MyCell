using System;
using System.Linq;
using MyCell.Structs;
using Unity.VisualScripting;
using UnityEngine;

namespace MyCell.Weapon.Components.ComponentData
{
    public class WeaponSpriteComponent : WeaponComponent
    {
        private SpriteRenderer _baseSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;

        private WeaponAttackPhase _currentAttackPhase;
        private int _currentSpriteIndex;
        
        private WeaponSpriteComponentData _data;

        private void Awake()
        {
            _baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();

            _currentAttackPhase = WeaponAttackPhase.Action;
        }

        private void SetAttackPhase(WeaponAttackPhase attackPhase)
        {
            _currentAttackPhase = attackPhase;
            _currentSpriteIndex = 0;
        }

        private void OnBaseSpriteRendererHandler(SpriteRenderer arg0)
        {
            var attackPhases = _data.GetAttackData(counter).attackPhases;
            var sprites = attackPhases.FirstOrDefault(item => item.phase == _currentAttackPhase).sprites;

            _weaponSpriteRenderer.sprite = sprites[_currentSpriteIndex];
            _currentSpriteIndex++;
            _currentSpriteIndex = _currentSpriteIndex >= sprites.Length ? 0 : _currentSpriteIndex;
        }

        private void OnEnable()
        {
            _baseSpriteRenderer.RegisterSpriteChangeCallback(OnBaseSpriteRendererHandler);
            _data = weapon.WeaponData.GetWeaponComponentData<WeaponSpriteComponentData>();
        }

        private void OnDisable()
        {
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(OnBaseSpriteRendererHandler);
        }
    }
}