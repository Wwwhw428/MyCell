using System;
using System.Collections;
using System.Collections.Generic;
using MyCell.Interfaces;
using MyCell.Utilities;
using MyCell.Weapon;
using MyCell.SO.Weapon;
using UnityEngine;

namespace MyCell.Weapon
{
    public class WeaponPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private WeaponDataSo data;

        private Bobber bobber;
        private SpriteRenderer graphics;

        private void Awake()
        {
            graphics = GetComponentInChildren<SpriteRenderer>();

            Init();

            bobber = GetComponentInChildren<Bobber>();
        }

        private void Init()
        {
            graphics.sprite = data.PickupSprite;
        }

        public object GetInteractionContext()
        {
            return data;
        }

        public void SetContext(object obj)
        {
            switch (obj)
            {
                case null:
                    gameObject.SetActive(false);
                    break;
                case WeaponDataSo so:
                    data = so;
                    Init();
                    break;
            }
        }

        public void EnableInteraction()
        {
            bobber.StartBobbing();
        }

        public void DisableInteraction()
        {
            bobber.StopBobbing();
        }

        public Vector3 GetPosition() => transform.position;
    }
}