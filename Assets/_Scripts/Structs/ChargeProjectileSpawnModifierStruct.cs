using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct ChargeProjectileSpawnModifierStruct : INameable
    {
        [HideInInspector] public string Name;
        public float AngleVariation;

        public void SetName(string value)
        {
            Name = value;
        }
    }
}