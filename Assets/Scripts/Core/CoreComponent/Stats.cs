using System;
using UnityEngine;

namespace MyCell.Core.CoreComponent
{
    public class Stats : CoreComponent
    {
        public event Action OnHealthZero;

        public PlayerStats_SO TemplateData;
        public PlayerStats_SO CharacterData;


        public int MaxHealth
        {
            get => CharacterData != null ? CharacterData.MaxHealth : 0;
            set => CharacterData.MaxHealth = value;
        }

        public int CurrentHealth
        {
            get => CharacterData != null ? CharacterData.CurrentHealth : 0;
            set => CharacterData.CurrentHealth = value;
        }

        public int BaseDefence
        {
            get => CharacterData != null ? CharacterData.BaseDefence : 0;
            set => CharacterData.BaseDefence = value;
        }

        public int CurrentDefence
        {
            get => CharacterData != null ? CharacterData.CurrentDefence : 0;
            set => CharacterData.CurrentDefence = value;
        }

        protected override void Awake()
        {
            base.Awake();
            if (TemplateData != null)
            {
                CharacterData = Instantiate(TemplateData);
            }
        }

        public void DecreaseHeath(int amount)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                OnHealthZero?.Invoke();

                Debug.Log("Health is zero!!");
            }
        }

        public void IncreaseHealth(int amount)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        }
    }
}

