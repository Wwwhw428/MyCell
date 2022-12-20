using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData_SO", menuName = "Data/Enemy/Base Data")]
public class EnemyData_SO : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10f;

    [Header("Ground Check")]
    public float GroundCheckRadius;
    public LayerMask WhatIsGround;

    [Header("Player Check")]
    public float PlayerCheckMinRange;
    public float PlayerCheckMaxRange;
    public LayerMask WhatIsPlayer;
}
