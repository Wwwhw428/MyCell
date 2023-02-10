using UnityEngine;
using MyCell.Structs;
using MyCell.Utilities;
using MyCell.Interfaces;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Modifiers
{
    public class BlockKnockbackModifier : KnockbackModifiers
    {
        private WeaponBlock _blockStruct;
        private Movement _movement;
        private Transform _transform;

        public BlockKnockbackModifier(WeaponBlock blockStruct, Movement movement, Transform transform)
        {
            this._blockStruct = blockStruct;
            this._movement = movement;
            this._transform = transform;
        }

        public override KnockbackData ModifyValue(KnockbackData value)
        {
            foreach (var blockDirection in _blockStruct.BlockDirections)
            {
                if (blockDirection.IsBetween(DetermineDamageSourceDirection(value.Source)))
                {
                    value.Strength *= 1 - blockDirection.KnockbackAbsorption;
                    break;
                }
            }

            return value;
        }
        
        private float DetermineDamageSourceDirection(GameObject source)
        {
            return CombatUtilities.AngleFromFacingDirection(_transform, source.transform, _movement.CurrentFaceDirection);
        }
    }
}