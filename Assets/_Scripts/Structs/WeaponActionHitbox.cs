using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct WeaponActionHitbox : INameable
    {
        [HideInInspector] public string name;
        public bool debug;
        [field: SerializeField]
        public Rect hitbox {get; private set;}
        public void SetName(string value)
        {
            name = value;
        }
    }
}
