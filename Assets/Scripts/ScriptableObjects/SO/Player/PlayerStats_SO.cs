using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStats Data", menuName = "Data/Player/Stats Data")]
public class PlayerStats_SO : ScriptableObject
{
    [Header("State Info")]
    public int MaxHealth;
    public int CurrentHealth;
    public int BaseDefence;
    public int CurrentDefence;
}
