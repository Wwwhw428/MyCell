using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.EnemyMachine.States
{
    public class EnemyMoveState : EnemyGroundState
    {
        private bool WallFront;
        private bool LedgeFront;
        private Enemy enemy;
        private string v;

        public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            DoCheck();

            if (WallFront || !LedgeFront)
            {
                flipAfterAnim = true;
                // StateMachine.ChangeState(Enemy.IdleState);
            }
            else
            {
                Movement.SetVelocityX(Enemy._enemyData.MovementVelocity * Movement.CurrentFaceDirection);
            }
        }

        public override void DoCheck()
        {
            base.DoCheck();
            WallFront = CollisionScene.WallFront;
            LedgeFront = CollisionScene.LedgeVertical;
        }
    }
}