using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.Interfaces;
using MyCell.Weapon;

namespace MyCell.Structs
{
    [System.Serializable]
    public struct WeaponBlock : INameable
    {
        [HideInInspector] public string AttackName;

        [field: SerializeField] public PhaseTime BlockWindowStart { get; private set; }
        [field: SerializeField] public PhaseTime BlockWindowEnd { get; private set; }

        [field: SerializeField] public GameObject BlockParticles { get; private set; }
        [field: SerializeField] public Vector2 BlockParticlesOffset { get; private set; }
        [field: SerializeField] public BlockDirection[] BlockDirections { get; private set; }

        public void SetName(string value)
        {
            AttackName = value;
        }
    }

    [System.Serializable]
    public struct PhaseTime
    {
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public WeaponAttackPhase Phase { get; private set; }

        public float TriggerTime { get; private set; }

        public bool SetTriggerTime(WeaponAttackPhase phase)
        {
            if (phase != Phase)
                return false;
            TriggerTime = Time.time + Duration;
            return true;
        }

        public bool IsPastTriggerTime => Time.time >= TriggerTime;
    }

    [System.Serializable]
    public struct BlockDirection
    {
        [Range(-180f, 180f)] public float MinAngle;
        [Range(-180f, 180f)] public float MaxAngle;
        [Range(0f, 1f)] public float DamageAbsorption;
        [Range(0f, 1f)] public float KnockbackAbsorption;
        [Range(0f, 1f)] public float PoiseDamageAbsorption;

        public bool IsBetween(float angle)
        {
            if (MaxAngle > MinAngle)
            {
                return angle >= MinAngle && angle <= MaxAngle;
            }

            return (angle >= MinAngle && angle <= 180f) || (angle <= MaxAngle && angle >= -180f);
        }
    }
}
