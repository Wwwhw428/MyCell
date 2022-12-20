using System.Collections;
using System.Collections.Generic;
using MyCell.Core;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region State Variable

    private EnemyStateMachine _stateMachine;
    public EnemyMoveState MoveState;
    public EnemyIdleState IdleState;
    public EnemyInAirState InAirState;
    public EnemyAttackState AttackState;
    public EnemyRemoteAttackState RemoteAttackState;
    public EnemySkillState SkillState;

    [SerializeField]
    public EnemyData_SO _enemyData;

    #endregion

    #region Check Variable

    public Transform PlayerCheck
    {
        get => GenericNotImplementedError<Transform>.TryGet(_playerCheck, transform.parent.name);
        private set => _playerCheck = value;
    }
    [SerializeField]
    private Transform _playerCheck;
    public bool CheckPlayerInMinRange
    {
        get => Physics2D.Raycast(PlayerCheck.position, transform.right, _enemyData.PlayerCheckMinRange, _enemyData.WhatIsPlayer);
    }
    public bool CheckPlayerInMaxRange
    {
        get => Physics2D.Raycast(PlayerCheck.position, transform.right, _enemyData.PlayerCheckMaxRange, _enemyData.WhatIsPlayer);
    }

    #endregion

    #region Component

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }

    #endregion

    #region Other Variable
    #endregion

    #region Unity Callback

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine();
        Anim = transform.GetComponent<Animator>();
        Core = GetComponentInChildren<Core>();

        // Ground State
        MoveState = new EnemyMoveState(this, _stateMachine, _enemyData, "Move");
        IdleState = new EnemyIdleState(this, _stateMachine, _enemyData, "Idle");
        InAirState = new EnemyInAirState(this, _stateMachine, _enemyData, "InAir");
        AttackState = new EnemyAttackState(this, _stateMachine, _enemyData, "Attack");
        RemoteAttackState = new EnemyRemoteAttackState(this, _stateMachine, _enemyData, "RemoteAttack");
        SkillState = new EnemySkillState(this, _stateMachine, _enemyData, "SkillAttack");
    }

    private void Start()
    {
        _stateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        Core.LogicUpdate();
        _stateMachine.CurrentState.LogicUpdata();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }

    #endregion

    #region Set Function
    #endregion

    #region Other Function

    public void AnimationTrigger() => _stateMachine.CurrentState.AnimationTrigger();
    public void AnimationFinishedTrigger() => _stateMachine.CurrentState.AnimationFinishedTrigger();
    public virtual void Attack()
    {
    }
    public virtual void RemoteAttack()
    {
    }
    public virtual void Skill()
    { 
    }
    #endregion
}