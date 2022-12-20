using MyCell.Structs;
using UnityEngine;

namespace MyCell.Weapon.Components.ComponentData
{
    public class WeaponSpriteComponentData : WeaponComponentData<WeaponSprites>
    {
        public WeaponSpriteComponentData()
        {
            Components.Add(typeof(WeaponSpriteComponent));
        }

        public override void OnValidate()
        {
            for (var i = 0; i < data.Length; i++)
            {
                for (var j = 0; j < data[i].attackPhases.Length; j++)
                {
                    data[i].attackPhases[j].name = data[i].attackPhases[j].phase.ToString();
                }
            }
        }
    }
}