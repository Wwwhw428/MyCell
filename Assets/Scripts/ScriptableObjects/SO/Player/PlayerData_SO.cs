using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Data/Player/Base Data")]
public class PlayerData_SO : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10f;

    [Header("Jump State")]
    public float JumpVelocity = 15f;
    public int AmountOfJump = 1;

    [Header("Ground Check")]
    public float GroundCheckRadius;
    public LayerMask WhatIsGround;

    [Header("Crunch State")]
    public float CrunchMovementVelocity = 5f;
    public float CeilingCheckRadius;
    public float StandColliderHeigh = 0.8f;
    public float CrunchColliderHeight = 0.46f;

    [Header("Wall Check")]
    public float WallCheckDistance;
    public float WallGrabTime = 1f;
    public float WallClimbTime = 1f;
    public float WallSlideTime = 1f;
    public float WallSlideVelocity = 5f;
    public float WallClimbVelocity = 5f;
    public float WallJumpTime;
    public Vector2 WallJumpAngle;

    [Header("Ledge State")]
    public Vector2 StartOffset;
    public Vector2 EndOffset;
}