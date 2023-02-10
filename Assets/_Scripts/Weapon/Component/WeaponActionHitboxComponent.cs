using System;
using UnityEngine;
using MyCell.CoreSystem.CoreComponent;
using MyCell.Weapon.Component.Data;
using MyCell.Structs;

namespace MyCell.Weapon.Component
{
    public class WeaponActionHitboxComponent : WeaponComponent<WeaponActionHitboxData>
    {
        public event Action<Collider2D[]> OnDetected;

        private Vector2 offset;

        private Movement Movement { get => _movement ?? core.GetCoreComponent(ref _movement); }
        private Movement _movement;

        private WeaponActionHitbox currentAttackData;

        private Collider2D[] detected;

        private void CheckHitbox()
        {
            // Set hitbox offset based on current position
            offset.Set(
              transform.position.x + (currentAttackData.hitbox.position.x * Movement.CurrentFaceDirection),
              transform.position.y + currentAttackData.hitbox.y
              );

            // Look for colliders in the hitbox
            detected = Physics2D.OverlapBoxAll(offset, currentAttackData.hitbox.size, 0f, componentData.DamageableLayers);

            if (detected.Length == 0) return;

            OnDetected?.Invoke(detected);
        }

        protected override void SetCurrentAttackData()
        {
            base.SetCurrentAttackData();
            currentAttackData = componentData.GetDataByIndex(currentAttackCount);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.AnimationHandler.OnAttackAction += CheckHitbox;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.AnimationHandler.OnAttackAction -= CheckHitbox;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var data = GetComponent<Weapon>().WeaponData?.GetComponentData<WeaponActionHitboxData>()?.GetAllData();

            if (data != null)
            {
                foreach (var item in data)
                {
                    if (item.debug)
                    {
                        Gizmos.DrawWireCube(transform.position + (Vector3)item.hitbox.position, (Vector3)item.hitbox.size);
                    }
                }
            }
        }
#endif
    }

    public class WeaponActionHitboxData : WeaponComponentData<WeaponActionHitbox>
    {
        [field: SerializeField] public LayerMask DamageableLayers { get; private set; }

        public WeaponActionHitboxData()
        {
            Components.Add(typeof(WeaponActionHitboxComponent));
        }
    }
}
