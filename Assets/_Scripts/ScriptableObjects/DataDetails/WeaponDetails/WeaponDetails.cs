using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.Weapon
{
    [System.Serializable]
    public class WeaponDetails
    {
        public int WeaponID;
        public string WeaponName;
        public Sprite WeaponIcon;
        public Sprite WeaponWorldSprite;
        public string WeaponDescription;
        public List<WeaponAttackDetails> WeaponAttackDetials = new List<WeaponAttackDetails>();
    }

    [System.Serializable]
    public class WeaponAttackDetails
    {
        public string AttackName;
        public int DamageOfAttack;
        public int CoolDownOfAttack;
        public Vector4 HitBox;
        public List<Sprite> Sprites = new List<Sprite>();
    }
}
