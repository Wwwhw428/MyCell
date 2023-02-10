using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public class WeaponDamage : INameable
    {
        [HideInInspector]
        public string name;
        public int damageAmount;
        public void SetName(string value)
        {
            name = value;
        }
    }
}
