using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.EnemyMachine.States
{
    public class EnemyIdleState : EnemyGroundState
    {

        public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Movement.SetVelocityX(0);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // TODO: if see target

            if (animationFinished)
            {
                if (flipAfterAnim)
                {
                    Movement.Flip();
                    flipAfterAnim = false;
                }
                // StateMachine.ChangeState(Enemy.MoveState);
            }

        }
    }
}