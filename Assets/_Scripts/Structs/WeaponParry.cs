using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct WeaponParry : INameable
    {
        [HideInInspector] public string attackName;

        [field: SerializeField] public float DamageAbsorption { get; private set; }
        [field: SerializeField] public float ParryWindowStart { get; private set; }
        [field: SerializeField] public float ParryWindowEnd { get; private set; }
        [field: SerializeField] public Rect ParryHitbox { get; private set; }

        public void SetName(string value)
        {
            attackName = value;
        }
    }
}
