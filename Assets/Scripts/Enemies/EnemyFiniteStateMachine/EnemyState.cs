using System.Collections;
using System.Collections.Generic;
using MyCell.Core;
using MyCell.Core.CoreComponent;
using UnityEngine;

public class EnemyState
{
    protected Core Core;

    public Enemy Enemy;
    public EnemyStateMachine StateMachine;
    public EnemyData_SO EnemyData;

    protected Movement Movement
    {
        get => _movement ?? Core.GetCoreComponent(ref _movement);
    }
    private Movement _movement;

    private string _animBoolName;
    private bool _isExitingState;

    protected float startTime;
    protected bool animationFinished;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName)
    {
        this.Enemy = enemy;
        this.Core = enemy.Core;
        this.StateMachine = stateMachine;
        this.EnemyData = enemyData;
        this._animBoolName = animBoolName;
    }

    public virtual void AnimationTrigger()
    {
        animationFinished = false;
    }

    public virtual void AnimationFinishedTrigger()
    {
        animationFinished = true;
    }

    public virtual void Enter()
    {
        Docheck();
        Enemy.Anim.SetBool(_animBoolName, true);
        animationFinished = false;
        _isExitingState = false;
        Debug.Log($"Enemy1 into {_animBoolName}");
    }

    public virtual void Exit()
    {
        Docheck();
        Enemy.Anim.SetBool(_animBoolName, false);
        _isExitingState = true;
        Debug.Log($"Enemy1 exit {_animBoolName}");
    }

    public virtual void LogicUpdata()
    {
        Docheck();
    }

    public virtual void PhysicsUpdate()
    {
        Docheck();
    }

    public virtual void Docheck()
    {
    }
}

