using System.Data.Common;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using MyCell.Interfaces;
using UnityEngine;

namespace MyCell.Weapon.Component.Data
{
    [System.Serializable]
    public abstract class WeaponComponentData
    {
        public List<Type> Components
        {
            get => _components;
            private set
            {
                _components = value;
            }
        }
        private List<Type> _components = new List<Type>();
        
        [field: SerializeField, HideInInspector]
        public string ComponentName {get; private set;}

        public WeaponComponentData()
        {
            ComponentName = this.GetType().Name;
        }

        public virtual void OnValidate() {}
    }

    [System.Serializable]
    public class WeaponComponentData<T> : WeaponComponentData where T : INameable
    {
        [field: SerializeField]
        protected T[] data;

        public T GetDataByIndex(int index)
        {
            return data[index];
        }

        public T[] GetAllData()
        {
            return data;
        }
    }
}
