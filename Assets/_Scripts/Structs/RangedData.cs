using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
  [System.Serializable]
  public struct RangedData : INameable
  {
    [HideInInspector]
    public string AttackName;
    public bool debug;
    public WeaponProjectileSpawnPoint[] AttackData;

    public void SetName(string value) => AttackName = value;
  }
}