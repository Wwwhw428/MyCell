using System;
using System.Collections;
using System.Collections.Generic;
using MyCell.SO.EventChannels;
using MyCell.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace MyCell.GUI
{
    public class PlayerWeaponUI : MonoBehaviour
    {
        [SerializeField] private CombatInput input;
        [SerializeField] private WeaponChangedEventChannel channel;

        public Image WeaponIcon;

        private void Awake()
        {
            WeaponIcon.color = Color.clear;
        }

        // private void Start()
        // {
        //     channel.OnEvent += HandleWeaponChangeEvent;
        // }
        private void OnEnable() => channel.OnEvent += HandleWeaponChangeEvent;
        private void OnDisable() => channel.OnEvent -= HandleWeaponChangeEvent;

        private void HandleWeaponChangeEvent(object sender, WeaponChangedEventArgs context)
        {
            Debug.Log($"Handle weapon Icon {context.WeaponInput}");
            if (context.WeaponInput == input && WeaponIcon)
            {
                WeaponIcon.sprite = context.WeaponData.PickupSprite;
                WeaponIcon.color = Color.white;
            }
        }
    }
}
