using System.Collections;
using System.Collections.Generic;
using MyCell.CoreSystem.CoreComponent;
using UnityEngine;

namespace MyCell.EnemyMachine.States
{
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

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            // if (isPlayerInMinRange)
            //     StateMachine.ChangeState(Enemy.AttackState);
            // else if (isPlayerInMaxRange)
            //     StateMachine.ChangeState(Enemy.RemoteAttackState);
        }

        public override void DoCheck()
        {
            base.DoCheck();
            // isPlayerInMinRange = Enemy.CheckPlayerInMinRange;
            // isPlayerInMinRange = Enemy.CheckPlayerInMaxRange;
        }
    }
}