using MyCell.Interfaces;
using MyCell.Weapon;
using UnityEngine;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct CriticalDamage : INameable
    {
        [HideInInspector]
        public string name;
        public PhaseCriticalDamage[] criticalData;

        public void SetName(string value)
        {
            name = value;
        }
    }

    [System.Serializable]
    public struct PhaseCriticalDamage
    {
        [HideInInspector]
        public string name;
        public WeaponAttackPhase phase;
        public int criticalMul;
        public int criticalAdd;
    }
}
