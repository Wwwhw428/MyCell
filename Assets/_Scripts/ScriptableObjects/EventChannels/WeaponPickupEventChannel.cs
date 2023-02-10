using System;
using MyCell.Weapon;
using MyCell.SO.Weapon;
using UnityEngine;

namespace MyCell.SO.EventChannels
{
    [CreateAssetMenu(fileName = "newWeaponPickupChannel", menuName = "Event Channels/Weapon Pickup")]
    public class WeaponPickupEventChannel : EventChannelsSO<WeaponPickupEventArgs>
    {
    }

    public class WeaponPickupEventArgs : EventArgs
    {
        public WeaponDataSo NewWeaponData;
        public WeaponDataSo PrimaryWeaponData;
        public WeaponDataSo SecondaryWeaponData;

        public WeaponPickupEventArgs(WeaponDataSo newWeaponData, WeaponDataSo primaryWeaponData, WeaponDataSo secondaryWeaponData)
        {
            NewWeaponData = newWeaponData;
            PrimaryWeaponData = primaryWeaponData;
            SecondaryWeaponData = secondaryWeaponData;
        }
    }
}