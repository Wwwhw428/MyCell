using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCell.EnemyMachine.States
{
    public class EnemySkillState : EnemyAbilityState
    {
        public EnemySkillState(Enemy enemy, EnemyStateMachine stateMachine, EnemyData_SO enemyData, string animBoolName) : base(enemy, stateMachine, enemyData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Enemy.Skill();
        }
    }
}