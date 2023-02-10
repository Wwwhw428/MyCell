using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;

namespace MyCell
{
    [System.Serializable]
    public struct WeaponKnockbackStruct : INameable
    {
        [HideInInspector] public string attackName;

        [field: SerializeField] public Vector2 KnockbackAngle { get; private set; }

        [field: SerializeField] public float KnockbackStrength { get; private set; }

        public void SetName(string value)
        {
            attackName = value;
        }
    }
}
