using System.Collections.Generic;
using MyCell.Interfaces;
using System;
using UnityEngine;

namespace MyCell.Weapon.Components.ComponentData
{
    [Serializable]
    public abstract class WeaponAttackDataComponent
    {
        public List<Type> Components
        {
            get => _components;
            protected set => _components = value;
        }
        private List<Type> _components = new List<Type>();
        
        public string ComponentName { get; private set; }  

        public WeaponAttackDataComponent()
        {
            ComponentName = this.GetType().Name;
        }
        
        public virtual void OnValidate() {}
    }
    [Serializable]
    public class WeaponComponentData<T> : WeaponAttackDataComponent where T : INameable
    {
        // If true, data is repeated for multiple attacks
        public bool repeatData;
        [field: SerializeField]
        protected T[] data;

        public T[] GetAllData() => data;

        public T GetAttackData(int i)
        {
            if (repeatData) i = 0;
            return data[i];
        }
    }
}