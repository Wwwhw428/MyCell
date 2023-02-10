using UnityEngine;
using MyCell.Weapon;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct WeaponMovement : INameable
    {
        [HideInInspector]
        public string name;
        public AttackPhaseMovement[] attackPhases;
        // TODO: NumberOfAttacks 调用 SetName
        public void SetName(string value)
        {
            name = value;
        }
    }

    [System.Serializable]
    public struct AttackPhaseMovement
    {
        [HideInInspector]
        public string name;
        public WeaponAttackPhase phase;
        public float speed;
        public Vector2 angle;
    }
}
