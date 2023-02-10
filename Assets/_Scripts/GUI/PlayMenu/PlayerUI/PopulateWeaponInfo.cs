using System;
using MyCell.Weapon;
using MyCell.SO.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyCell.GUI
{
    public class PopulateWeaponInfo: MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;

        public void SetWeaponInfo(WeaponDataSo data)
        {
            weaponIcon.sprite = data.PickupSprite;
            weaponName.text = data.WeaponName;
            weaponDescription.text = data.WeaponDescription;
        }
    }
}
