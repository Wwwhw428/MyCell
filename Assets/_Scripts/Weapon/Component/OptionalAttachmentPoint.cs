using UnityEngine;
using MyCell.Structs;
using MyCell.Weapon.Component.Data;

namespace MyCell.Weapon.Component
{
    public class OptionalAttachmentPoint : WeaponComponent<OptionalAttachmentPointData>
    {
        private SpriteRenderer optionalSpriteRenderer;

        public override void Init()
        {
            base.Init();

            optionalSpriteRenderer = transform.Find("Base/OptionalSprite")?.GetComponent<SpriteRenderer>();
            optionalSpriteRenderer.enabled = false;
        }

        private void Enter()
        {
            var currentAttackData = componentData.GetDataByIndex(currentAttackCount);

            if (currentAttackData.UseSprite)
            {
                optionalSpriteRenderer.sprite = currentAttackData.Sprite;
            }
        }

        private void Exit()
        {
            optionalSpriteRenderer.sprite = null;
        }

        private void EnableSpite() => optionalSpriteRenderer.enabled = true;
        private void DisableSpite() => optionalSpriteRenderer.enabled = false;

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.AnimationHandler.OnEnableOptionalSprite += EnableSpite;
            weapon.AnimationHandler.OnDisableOptionalSprite += DisableSpite;
            weapon.OnEnter += Enter;
            weapon.OnExit += Exit;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.AnimationHandler.OnEnableOptionalSprite -= EnableSpite;
            weapon.AnimationHandler.OnDisableOptionalSprite -= DisableSpite;
            weapon.OnEnter -= Enter;
            weapon.OnExit -= Exit;
        }
    }

    public class OptionalAttachmentPointData : WeaponComponentData<OptionalAttachmentPointStruct>
    {
        public OptionalAttachmentPointData() => Components.Add(typeof(OptionalAttachmentPoint));
    }
}