using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Weapon.Component.Data;
using MyCell.Weapon.Component;

namespace MyCell.SO.Weapon
{
    [CreateAssetMenu(fileName = "WeaponDataSo", menuName = "Data/WeaponDataSo")]
    public class WeaponDataSo : ScriptableObject
    {

        public string WeaponName;
        public string WeaponDescription;
        public Sprite PickupSprite;
        public int NumberOfAttack;
        public float CoolingTime;
        public RuntimeAnimatorController AnimatorController;

        [SerializeReference]
        private List<WeaponComponentData> componentData = new List<WeaponComponentData>();
        public List<WeaponComponentData> ComponentData
        {
            get => componentData;
            set
            {
                componentData = value;
            }
        }

        private void Awake()
        {
#if UNITY_EDITOR
            AddComponentData(new WeaponSpriteData());
#endif
        }

        public T GetComponentData<T>() where T : WeaponComponentData
        {
            return componentData.OfType<T>().FirstOrDefault();
        }

#if UNITY_EDITOR
        public void AddComponentData<T>(T data) where T : WeaponComponentData
        {
            if (ComponentData.FirstOrDefault(item => item.GetType() == data.GetType()) != null)
                return;
            ComponentData.Add(data);
        }
#endif

        public List<Type> GetAllComponents()
        {
            List<Type> components = new List<Type>();
            foreach (var data in ComponentData)
            {
                foreach (var component in data.Components)
                {
                    components.Add(component);
                }
            }

            return components;
        }

        private void OnValidate()
        {
            ComponentData.ForEach(item => item.OnValidate());
        }
    }
}
