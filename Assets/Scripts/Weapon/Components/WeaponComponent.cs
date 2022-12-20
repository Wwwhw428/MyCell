using System;
using Unity.VisualScripting;
using UnityEngine;

namespace MyCell.Weapon.Components
{
    public class WeaponComponent : MonoBehaviour
    {
        public Weapon weapon;
        public Core.Core core;

        public int counter;

        public void SetCounter(int value) => counter = value;
        
        private void Awake()
        {
            weapon = GetComponent<Weapon>();
            core = weapon.Core;
        }

        private void OnEnable()
        {
            weapon.OnCounterChange += SetCounter;
        }

        private void OnDisable()
        {
            weapon.OnCounterChange -= SetCounter;
        }
    }
}
