using MyCell.Interfaces;
using MyCell.Weapon;
using UnityEngine;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct WeaponSprites : INameable
    {
        [HideInInspector]
        public string name;
        public AttackPhaseSprites[] attackPhases;
        // TODO: NumberOfAttacks 调用 SetName
        public void SetName(string value)
        {
            name = value;
        }
    }

    [System.Serializable]
    public struct AttackPhaseSprites
    {
        [HideInInspector]
        public string name;
        public WeaponAttackPhase phase;
        public Sprite[] sprites;
    }
}