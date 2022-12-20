using System.Collections;
using System.Collections.Generic;
using MyCell.Core.CoreComponent;
using UnityEngine;

public class EnemyGroundState : EnemyState
{
    protected CollisionScene CollisionScene
    {
        get => _collisionScene ?? Core.GetCoreComponent(ref _collisionScene);
    }
    private CollisionScene _collisionScene;

    protected static bool flipAfterAnim;

    protected bool isPlayerInMinRange;
    protected bool isPlayerInMaxRange;

    public EnemyGroundState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void LogicUpdata()
    {
        base.LogicUpdata();

        if (isPlayerInMinRange)
            StateMachine.ChangeState(Enemy.AttackState);
        else if (isPlayerInMaxRange)
            StateMachine.ChangeState(Enemy.RemoteAttackState);
    }

    public override void Docheck()
    {
        base.Docheck();
        isPlayerInMinRange = Enemy.CheckPlayerInMinRange;
        isPlayerInMinRange = Enemy.CheckPlayerInMaxRange;
    }
}
