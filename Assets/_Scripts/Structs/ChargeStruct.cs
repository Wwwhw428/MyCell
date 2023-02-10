using UnityEngine;
using MyCell.Interfaces;

namespace MyCell.Structs
{
  [System.Serializable]
  public struct ChargeStruct : INameable
  {
    [HideInInspector]
    public string Name;

    public bool StartWithOne;
    
    public float ChargeTime;
    public int NumOfCharges;

    public GameObject ChargeParticlesPrefab;
    public GameObject MaxChargeParticlesPrefab;

    public Vector2 Offset;

    public void SetName(string value) => Name = value;

  }
}