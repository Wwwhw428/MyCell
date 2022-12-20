using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyGroundState
{
    private bool WallFront;
    private bool LedgeFront;

    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
    {
    }

    public override void LogicUpdata()
    {
        base.LogicUpdata();
        Docheck();

        if (WallFront || !LedgeFront)
        {
            flipAfterAnim = true;
            StateMachine.ChangeState(Enemy.IdleState);
        }
        else
        {
            Movement.SetVelocityX(Enemy._enemyData.MovementVelocity * Movement.CurrentFaceDirection);
        }
    }

    public override void Docheck()
    {
        base.Docheck();
        WallFront = CollisionScene.WallFront;
        LedgeFront = CollisionScene.LedgeVertical;
    }
}

