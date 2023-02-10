using UnityEngine;
using MyCell.Structs;
using MyCell.Utilities;
using MyCell.Interfaces;
using MyCell.CoreSystem.CoreComponent;

namespace MyCell.Modifiers
{
    public class BlockPoiseDamageModifier : PoiseDamageModifier
    {
        private readonly WeaponBlock _blockStruct;
        private readonly Movement _movement;
        private readonly Transform _entityTransform;

        public BlockPoiseDamageModifier(WeaponBlock blockStruct, Movement movement, Transform entityTransform)
        {
            this._blockStruct = blockStruct;
            this._movement = movement;
            this._entityTransform = entityTransform;
        }

        public override PoiseDamageData ModifyValue(PoiseDamageData value)
        {
            foreach (var blockDirection in _blockStruct.BlockDirections)
            {
                if(!blockDirection.IsBetween(CombatUtilities.AngleFromFacingDirection(_entityTransform, value.Source.transform, _movement.CurrentFaceDirection)))
                    continue;
                value.PoiseDamageAmount *= 1 - blockDirection.PoiseDamageAbsorption;
            }

            return value;
        }
    }
}