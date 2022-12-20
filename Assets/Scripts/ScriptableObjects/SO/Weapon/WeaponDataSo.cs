using System;
using System.Collections.Generic;
using System.Linq;
using MyCell.Weapon.Components.ComponentData;
using UnityEngine;

namespace MyCell.ScriptableObjects.SO.Weapon
{
    [CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Data/Weapon/WeaponDataSo")]
    public class WeaponDataSo : ScriptableObject
    {
        public string weaponName;
        public string weaponDescription;
        public int numberOfAttack;
        
        public RuntimeAnimatorController AnimatorController { get; private set; }

        public List<WeaponAttackDataComponent> ComponentData => _componentData;
        [SerializeReference] private List<WeaponAttackDataComponent> _componentData = new List<WeaponAttackDataComponent>();
        
        private void Awake()
        {
#if UNITY_EDITOR
                AddWeaponComponentData(new WeaponSpriteComponentData());
#endif
        }

        public List<Type> GetAllWeaponComponent()
        {
            List<Type> components = new List<Type>();

            foreach (var componentData in ComponentData)
            {
                foreach (var component in componentData.Components)
                {
                    if (components.FirstOrDefault(item => item.GetType() == component) == null)
                    {
                        components.Add(component);
                    }         
                }
            }

            return components;
        }

        public T GetWeaponComponentData<T>() where T : WeaponAttackDataComponent
        {
            return _componentData.OfType<T>().FirstOrDefault();
        }
        
#if UNITY_EDITOR
        public void AddWeaponComponentData<T>(T data) where T : WeaponAttackDataComponent
        {
            if (ComponentData.FirstOrDefault(item => item.GetType() == data.GetType()) != null)
            {
                Debug.LogWarning($"Weapon already has {data.GetType().Name}");
                return;
            }

            ComponentData.Add(data);
        }
#endif
    }
}