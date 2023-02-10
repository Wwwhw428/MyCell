using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCell.EnemyMachine.States;

namespace MyCell.EnemyMachine.Specific
{
    public class Enemy_2 : Enemy
    {
        public EnemyMoveState MoveState;
        public EnemyIdleState IdleState;
        public EnemyInAirState InAirState;
        public EnemyAttackState AttackState;
        public EnemyRemoteAttackState RemoteAttackState;
        public EnemySkillState SkillState;

        protected override void Awake()
        {
            base.Awake();

            // Ground State
            MoveState = new EnemyMoveState(this, stateMachine, _enemyData, "Move");
            IdleState = new EnemyIdleState(this, stateMachine, _enemyData, "Idle");
            InAirState = new EnemyInAirState(this, stateMachine, _enemyData, "InAir");
            AttackState = new EnemyAttackState(this, stateMachine, _enemyData, "Attack");
            RemoteAttackState = new EnemyRemoteAttackState(this, stateMachine, _enemyData, "RemoteAttack");
            SkillState = new EnemySkillState(this, stateMachine, _enemyData, "SkillAttack");
        }

        protected override void Start()
        {
            base.Start();

            stateMachine.Initialize(IdleState);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
