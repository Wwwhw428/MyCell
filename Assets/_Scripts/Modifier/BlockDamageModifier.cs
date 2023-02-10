using System;
using UnityEngine;
using MyCell.Structs;
using MyCell.Utilities;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Modifiers
{
    public class BlockDamageModifier : DamageModifier
    {
        private readonly WeaponBlock _blockStruct;
        private readonly Movement _movement;
        private readonly Transform _transform;

        public event Action<GameObject> OnBlock;

        public BlockDamageModifier(WeaponBlock blockStruct, Movement movement, Transform transform)
        {
            this._blockStruct = blockStruct;
            this._movement = movement;
            this._transform = transform;
        }

        public override DamageData ModifyValue(DamageData value)
        {
            foreach (var blockDirection in _blockStruct.BlockDirections)
            {
                if (!blockDirection.IsBetween(DetermineDamageSourceDirection(value.Source))) continue;
                value.DamageAmount *= 1 - blockDirection.DamageAbsorption;
                OnBlock?.Invoke(value.Source);
                break;
            }

            return value;
        }

        private float DetermineDamageSourceDirection(GameObject source)
        {
            return CombatUtilities.AngleFromFacingDirection(_transform, source.transform, _movement.CurrentFaceDirection);
        }
    }
}