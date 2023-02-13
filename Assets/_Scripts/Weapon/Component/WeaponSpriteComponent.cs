using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MyCell.Weapon.Component.Data;
using MyCell.Structs;

namespace MyCell.Weapon.Component
{
    public class WeaponSpriteComponent : WeaponComponent<WeaponSpriteData>
    {
        #region Component Variable

        private SpriteRenderer _baseSpriteRenderer;
        private SpriteRenderer _WeaponSpriteRenderer;

        #endregion

        #region Other Variable

        private int _currentSpriteCount;
        private WeaponAttackPhase _currentAttackPhase = WeaponAttackPhase.Action;
        private Sprite[] _sprites;

        #endregion

        public override void Init()
        {
            base.Init();
            _baseSpriteRenderer = weapon.BaseGo.GetComponent<SpriteRenderer>();
            _WeaponSpriteRenderer = weapon.WeaponSpriteGo.GetComponent<SpriteRenderer>();
            _baseSpriteRenderer.RegisterSpriteChangeCallback(SpriteChangeHandler);
        }

        #region Unity Callback

        protected override void OnEnable()
        {
            base.OnEnable();
            _baseSpriteRenderer.RegisterSpriteChangeCallback(SpriteChangeHandler);
            weapon.AnimationHandler.OnAttackPhaseChanged += SetPhase;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _baseSpriteRenderer.sprite = null;
            _WeaponSpriteRenderer.sprite = null;
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(SpriteChangeHandler);
            weapon.AnimationHandler.OnAttackPhaseChanged -= SetPhase;
        }

        #endregion

        #region Set Function

        public void SetPhase(WeaponAttackPhase phase)
        {
            _currentAttackPhase = phase;
            _currentSpriteCount = 0;
            _sprites = componentData.GetDataByIndex(currentAttackCount).attackPhases.FirstOrDefault(item => item.phase == phase).sprites;
        }

        #endregion

        #region Handler Function

        private void SpriteChangeHandler(SpriteRenderer sr)
        {
            // if(_sprites == null)
            //     Debug.Log($"{_currentAttackPhase.ToString()} {_currentSpriteCount}");
            if (_currentSpriteCount + 1 > _sprites.Length)
            {
                _WeaponSpriteRenderer.sprite = null;
                return;
            }
            else
                _WeaponSpriteRenderer.sprite = _sprites[_currentSpriteCount];

            _currentSpriteCount = ++_currentSpriteCount >= _sprites.Length ? 0 : _currentSpriteCount;
        }

        #endregion
    }

    public class WeaponSpriteData : WeaponComponentData<WeaponSprites>
    {
        public WeaponSpriteData()
        {
            Components.Add(typeof(WeaponSpriteComponent));
        }

        public override void OnValidate()
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].attackPhases.Length; j++)
                {
                    data[i].attackPhases[j].name = data[i].attackPhases[j].phase.ToString();
                }
            }
        }
    }
}
