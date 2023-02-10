using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;
using MyCell.Weapon;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct AttackAmountCriticalData : INameable
    {
        [HideInInspector]
        public string name;
        public AttackAmountCriticalPhase[] phases;

        public void SetName(string value)
        {
            name = value;
        }
    }
    
    [System.Serializable]
    public struct AttackAmountCriticalPhase
    {
        [HideInInspector]
        public string name;
        public WeaponAttackPhase phase;
        public bool isCritical;
    }
}
