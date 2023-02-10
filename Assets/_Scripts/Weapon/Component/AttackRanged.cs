using UnityEngine;
using System;
using MyCell.Structs;
using MyCell.Projectiles;
using MyCell.Weapon.Component.Data;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Weapon.Component
{
    public class AttackRanged : WeaponComponent<AttackRangedData>
    {
        public event Action<GameObject> OnProjectileSpawned;

        public event Func<int, int> OnSetNumberOfProjectiles;
        public event Func<Vector2, Vector2[]> OnSetProjectileDirection;

        private Vector2 _offset;
        private Vector2 _direction;

        private Movement Movement => _movement ?? core.GetCoreComponent(ref _movement);
        private Movement _movement;

        private Transform projectileContainer;

        private int numberToSpawn = 1;

        private void SpawnProjectiles()
        {
            var curAtkData = componentData.GetDataByIndex(currentAttackCount);

            foreach (var point in curAtkData.AttackData)
            {
                int num2spwn = OnSetNumberOfProjectiles?.Invoke(numberToSpawn) ?? numberToSpawn;

                var position = transform.position;
                _offset.Set(
                    position.x + point.offset.x * Movement.CurrentFaceDirection,
                    position.y + point.offset.y
                );

                _direction.Set(point.direction.x * Movement.CurrentFaceDirection, point.direction.y);

                var directions = OnSetProjectileDirection?.Invoke(_direction) ?? new Vector2[] { _direction };

                for (int i = 0; (i < num2spwn) || (i < directions.Length); i++)
                {
                    var projectile = Instantiate(
                        point.projectileData.ProjectilePrefab,
                        _offset,
                        Quaternion.Euler(0f, 0f, Utilities.VectorUtilities.AngleFromVector2(directions[i])),
                        projectileContainer);

                    var projectileScript = projectile.GetComponent<Projectile>();
                    projectileScript.CreateProjectile(point.projectileData);

                    OnProjectileSpawned?.Invoke(projectile);

                    projectileScript.Init(core.Parent);
                }
            }
        }

        public override void Init()
        {
            base.Init();
            projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.AnimationHandler.OnAttackAction += SpawnProjectiles;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.AnimationHandler.OnAttackAction -= SpawnProjectiles;
        }

        private void OnDrawGizmos()
        {
            var weaponScript = GetComponent<Weapon>();
            if (weaponScript == null) return;

            var allData = weaponScript.WeaponData.GetComponentData<AttackRangedData>().GetAllData();

            if (allData == null) return;

            foreach (var item in allData)
            {
                if (!item.debug) continue;
                foreach (var point in item.AttackData)
                {
                    var pos = transform.position + (Vector3)point.offset;

                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(pos, pos + (Vector3)point.direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }
    }

    [Serializable]
    public class AttackRangedData : WeaponComponentData<RangedData>
    {
        public AttackRangedData() => Components.Add(typeof(AttackRanged));
    }
}