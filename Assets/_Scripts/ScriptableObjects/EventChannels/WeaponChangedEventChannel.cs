using System;
using MyCell.Weapon;
using MyCell.SO.Weapon;
using UnityEngine;

namespace MyCell.SO.EventChannels
{
    [CreateAssetMenu(fileName = "newWeaponChangedChannel", menuName = "Event Channels/Weapon Changes")]
    public class WeaponChangedEventChannel : EventChannelsSO<WeaponChangedEventArgs>{ }

    public class WeaponChangedEventArgs : EventArgs
    {
        public WeaponDataSo WeaponData;
        public CombatInput WeaponInput;

        public WeaponChangedEventArgs(WeaponDataSo weaponData, CombatInput weaponInput)
        {
            WeaponData = weaponData;
            WeaponInput = weaponInput;
        }
    }
}