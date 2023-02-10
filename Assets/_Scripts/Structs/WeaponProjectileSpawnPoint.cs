using UnityEngine;
using MyCell.SO.Projetile;

namespace MyCell.Structs
{
  [System.Serializable]
  public struct WeaponProjectileSpawnPoint
  {
    public Vector2 offset;
    public Vector2 direction;
    public ProjectileDataSO projectileData;
  }
}