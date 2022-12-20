using System.IO;
using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using MyCell.Core;
using UnityEngine;
using MyCell.Weapon;

public class Player : MonoBehaviour
{
    #region State Variable
    private PlayerStateMachine _stateMachine;
    // InAir State
    public PlayerInAirState InAirState { get; private set; }
    // Ability State
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    // Ground State
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerCrunchIdleState CrunchIdleState { get; private set; }
    public PlayerCrunchMoveState CrunchMoveState { get; private set; }
    // Touching Wall State
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }
    public PlayerWallGrabState WallGrabState { get; private set; }
    // Ledge State
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    // Attack State
    public PlayerAttackState PrimaryAttackState { get; private set; }
    public PlayerAttackState SecondaryAttackState { get; private set; }

    [SerializeField]
    private PlayerData_SO _playerData;
    #endregion

    #region Component
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    [HideInInspector]
    public PlayerInputHandler InputHandler;
    public BoxCollider2D MovementCollider { get; private set; }
    #endregion

    #region Other Variable
    private Vector2 _vector2WorkSpace;
    private Weapon _primaryWeapon;
    private Weapon _secondaryWeapon;
    #endregion

    #region  Unity Callback
    private void Awake()
    {
        _stateMachine = new PlayerStateMachine();

        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        MovementCollider = GetComponent<BoxCollider2D>();
        Core = GetComponentInChildren<Core>();

        _primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        _secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();

        // InAir State
        InAirState = new PlayerInAirState(this, _stateMachine, _playerData, "InAir");
        // Ability State
        JumpState = new PlayerJumpState(this, _stateMachine, _playerData, "InAir");
        WallJumpState = new PlayerWallJumpState(this, _stateMachine, _playerData, "InAir");
        // Ground State
        IdleState = new PlayerIdleState(this, _stateMachine, _playerData, "Idle");
        MoveState = new PlayerMoveState(this, _stateMachine, _playerData, "Move");
        LandState = new PlayerLandState(this, _stateMachine, _playerData, "Land");
        CrunchIdleState = new PlayerCrunchIdleState(this, _stateMachine, _playerData, "CrunchIdle");
        CrunchMoveState = new PlayerCrunchMoveState(this, _stateMachine, _playerData, "CrunchMove");
        // Touching Wall State
        WallSlideState = new PlayerWallSlideState(this, _stateMachine, _playerData, "WallSlide");
        WallClimbState = new PlayerWallClimbState(this, _stateMachine, _playerData, "WallClimb");
        WallGrabState = new PlayerWallGrabState(this, _stateMachine, _playerData, "WallGrab");
        // Ledge State
        LedgeClimbState = new PlayerLedgeClimbState(this, _stateMachine, _playerData, "LedgeClimbState");
        // Attack State
        PrimaryAttackState = new PlayerAttackState(this, _stateMachine, _playerData, "Attack", _primaryWeapon);
        SecondaryAttackState = new PlayerAttackState(this, _stateMachine, _playerData, "Attack", _secondaryWeapon);
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        Core.LogicUpdate();
        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Function
    public void SetColliderHeight(float height)
    {
        Vector2 center = MovementCollider.offset;
        _vector2WorkSpace.Set(MovementCollider.size.x, height);
        center.y += (height - MovementCollider.size.y) / 2;

        MovementCollider.offset = center;
        MovementCollider.size = _vector2WorkSpace;
    }
    #endregion

    #region Other Funtion

    public void AnimationTrigger() => _stateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishedTrigger() => _stateMachine.CurrentState.AnimationFinishedTrigger();

    #endregion
}